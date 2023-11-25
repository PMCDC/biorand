﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IntelOrca.Biohazard.Script.Opcodes;

namespace IntelOrca.Biohazard.BioRand.Events
{
    internal class CsPlot
    {
        public SbProcedure Root { get; }

        public CsPlot(SbProcedure root)
        {
            Root = root;
        }
    }

    internal class CsEntity
    {
        public byte Id { get; set; }

        public CsEntity(byte id)
        {
            Id = id;
        }
    }

    internal class CsEnemy : CsEntity
    {
        public byte Type { get; }
        public byte GlobalId { get; }
        public REPosition DefaultPosition { get; }
        public Action<SceEmSetOpcode> ProcessFunc { get; }

        public CsEnemy(byte id, byte globalId, byte type, REPosition defaultPosition, Action<SceEmSetOpcode> processFunc)
            : base(id)
        {
            GlobalId = globalId;
            Type = type;
            DefaultPosition = defaultPosition;
            ProcessFunc = processFunc;
        }
    }

    internal interface ICsHero
    {
        string Actor { get; }
    }

    internal class CsAlly : CsEntity, ICsHero
    {
        public byte Type { get; }
        public string Actor { get; }

        public CsAlly(byte id, byte type, string actor)
            : base(id)
        {
            Type = type;
            Actor = actor;
        }
    }

    internal class CsPlayer : ICsHero
    {
        public string Actor { get; } = "leon";
    }

    internal class CsFlag
    {
        public ReFlag Flag { get; set; }

        public CsFlag() { }

        public CsFlag(ReFlag flag)
        {
            Flag = flag;
        }

        public CsFlag(byte type, byte index)
        {
            Flag = new ReFlag(type, index);
        }
    }

    internal class CsAot
    {
        public byte Id { get; }

        public CsAot(byte id)
        {
            Id = id;
        }
    }

    internal class CsItem : CsAot
    {
        public byte GlobalId { get; } = 255;
        public Item Item { get; }

        public CsItem(byte id, byte globalId, Item item)
            : base(id)
        {
            GlobalId = globalId;
            Item = item;
        }
    }

    internal abstract class SbNode
    {
        public virtual IEnumerable<SbNode> Children => Enumerable.Empty<SbNode>();
        public virtual void Build(CutsceneBuilder builder)
        {
            foreach (var child in Children)
            {
                child.Build(builder);
            }
        }

        public static SbNode Conditional(bool value, Func<SbNode> callback)
        {
            return value ? callback() : new SbNop();
        }
    }

    internal class SbNop : SbNode
    {
    }

    internal class SbContainerNode : SbNode
    {
        private SbNode[] _children;

        public SbContainerNode(params SbNode[] children)
        {
            _children = children;
        }

        public override IEnumerable<SbNode> Children => _children;
    }

    internal class SbCommentNode : SbContainerNode
    {
        public string Description { get; }

        public SbCommentNode(string description, params SbNode[] children)
            : base(children)
        {
            Description = description;
        }
    }

    internal class SbSleep : SbNode
    {
        public int Ticks { get; }

        public SbSleep(int ticks)
        {
            Ticks = ticks;
        }

        public override void Build(CutsceneBuilder builder)
        {
            if (Ticks == 0)
            {
            }
            else if (Ticks == 1)
            {
                builder.Sleep1();
            }
            else
            {
                builder.Sleep(Ticks);
            }
        }
    }

    internal class SbCut : SbContainerNode
    {
        public int Cut { get; }

        public SbCut(int cut, params SbNode[] children)
            : base(children)
        {
            Cut = cut;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.CutChange(Cut);
            base.Build(builder);
            builder.CutRevert();
        }
    }

    internal class SbCutRevert : SbNode
    {
        public override void Build(CutsceneBuilder builder)
        {
            builder.CutRevert();
        }
    }

    internal class SbMuteMusic : SbContainerNode
    {
        public SbMuteMusic(params SbNode[] children)
            : base(children)
        {
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.FadeOutMusic();
            base.Build(builder);
            builder.ResumeMusic();
        }
    }

    internal class SbSetFade : SbNode
    {
        private byte _a;
        private byte _b;
        private byte _c;
        private byte _d;
        private byte _e;

        public SbSetFade(byte a, byte b, byte c, byte d, byte e)
        {
            _a = a;
            _b = b;
            _c = c;
            _d = d;
            _e = e;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.SetFade(_a, _b, _c, _d, _e);
        }
    }

    internal class SbAdjustFade : SbNode
    {
        private byte _a;
        private byte _b;
        private byte _c;

        public SbAdjustFade(byte a, byte b, byte c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.AdjustFade(_a, _b, _c);
        }
    }

    internal class SbAotOn : SbNode
    {
        public CsAot Aot { get; }

        public SbAotOn(CsAot aot)
        {
            Aot = aot;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.AotOn(Aot.Id);
        }
    }

    internal class SbDoor : SbNode
    {
        public PointOfInterest Poi { get; }

        public SbDoor(PointOfInterest poi)
        {
            Poi = poi;
        }

        public override void Build(CutsceneBuilder builder)
        {
            var poi = Poi;
            var pos = poi.Position;
            if (poi.HasTag("door"))
            {
                builder.PlayDoorSoundOpen(pos);
                builder.Sleep(30);
                builder.PlayDoorSoundClose(pos);
            }
            else
            {
                builder.Sleep(30);
            }
        }
    }

    internal class SbEntityTravel : SbNode
    {
        public CsEntity Entity { get; }
        public ReFlag CompletionFlag { get; }
        public REPosition Destination { get; }
        public PlcDestKind Kind { get; }

        public SbEntityTravel(
            CsEntity entity,
            ReFlag completionFlag,
            REPosition destination,
            PlcDestKind kind)
        {
            Entity = entity;
            CompletionFlag = completionFlag;
            Destination = destination;
            Kind = kind;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.WorkOnEnemy(Entity.Id);
            builder.AppendLine("plc_dest", 0, (byte)Kind, CompletionFlag.Index, Destination.X, Destination.Z);
            builder.AppendLine("plc_rot", 0, 256);
        }
    }

    internal class SbLockPlot : SbContainerNode
    {
        public SbLockPlot(params SbNode[] children)
            : base(children)
        {
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.SetFlag(CutsceneBuilder.FG_ROOM, 23);
            base.Build(builder);
            builder.SetFlag(CutsceneBuilder.FG_ROOM, 23, false);
        }
    }

    internal class SbLockControls : SbContainerNode
    {
        public SbLockControls(params SbNode[] children)
            : base(children)
        {
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.SetFlag(CutsceneBuilder.FG_STOP, 7, true);
            base.Build(builder);
            builder.SetFlag(CutsceneBuilder.FG_STOP, 7, false);
        }
    }

    internal class SbCutsceneBars : SbContainerNode
    {
        public SbCutsceneBars(params SbNode[] children)
            : base(children)
        {
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.SetFlag(CutsceneBuilder.FG_STATUS, 27, true);
            builder.SetFlag(CutsceneBuilder.FG_STOP, 7, true);
            base.Build(builder);
            builder.SetFlag(CutsceneBuilder.FG_STOP, 7, false);
            builder.SetFlag(CutsceneBuilder.FG_STATUS, 27, false);
        }
    }

    internal class SbAlly : SbNode
    {
        public CsAlly Ally { get; }
        public REPosition Position { get; }

        public SbAlly(CsAlly ally, REPosition position)
        {
            Ally = ally;
            Position = position;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.Ally(Ally.Id, Ally.Type, Position);
        }
    }

    internal class SbEnemy : SbNode
    {
        private readonly CsEnemy _enemy;
        private readonly REPosition _position;
        private readonly bool _enabled;
        private readonly byte? _pose;

        public SbEnemy(CsEnemy enemy, bool enabled = true, byte? pose = null)
            : this(enemy, enemy.DefaultPosition, enabled, pose)
        {
        }

        public SbEnemy(CsEnemy enemy, REPosition position, bool enabled = true, byte? pose = null)
        {
            _enemy = enemy;
            _position = position;
            _enabled = enabled;
            _pose = pose;
        }

        public override void Build(CutsceneBuilder builder)
        {
            var opcode = new SceEmSetOpcode();
            opcode.Id = _enemy.Id;
            opcode.Ai = (byte)(_enabled ? 0 : 128);
            opcode.X = (short)_position.X;
            opcode.Y = (short)_position.Y;
            opcode.Z = (short)_position.Z;
            opcode.D = (short)_position.D;
            opcode.Floor = (byte)_position.Floor;
            opcode.KillId = _enemy.GlobalId;
            opcode.Type = _enemy.Type;
            _enemy.ProcessFunc(opcode);
            if (_pose is byte pose)
            {
                opcode.State = pose;
            }
            builder.Enemy(opcode);
        }
    }

    internal class SbMoveEntity : SbNode
    {
        public CsEntity Entity { get; }
        public REPosition Position { get; }

        public SbMoveEntity(CsEntity entity, REPosition position)
        {
            Entity = entity;
            Position = position;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.MoveEnemy(Entity.Id, Position);
        }
    }

    internal class SbSetEntityCollision : SbNode
    {
        public CsEntity Entity { get; }
        public bool Value { get; }

        public SbSetEntityCollision(CsEntity entity, bool value)
        {
            Entity = entity;
            Value = value;
        }

        public override void Build(CutsceneBuilder builder)
        {
            if (Value)
                builder.EnableEnemyCollision(Entity.Id);
            else
                builder.DisableEnemyCollision(Entity.Id);
        }
    }

    internal class SbSetEntityNeck : SbNode
    {
        public CsEntity Entity { get; }
        public int Speed { get; }

        public SbSetEntityNeck(CsEntity entity, int speed)
        {
            Entity = entity;
            Speed = speed;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.SetEnemyNeck(Entity.Id, Speed);
        }
    }

    internal class SbSetEntityEnabled : SbNode
    {
        public CsEntity Entity { get; }
        public bool Enabled { get; }

        public SbSetEntityEnabled(CsEntity entity, bool enabled)
        {
            Entity = entity;
            Enabled = enabled;
        }

        public override void Build(CutsceneBuilder builder)
        {
            var id = Entity.Id;
            if (Enabled)
                builder.ActivateEnemy(id);
            else
                builder.DeactivateEnemy(id);
        }
    }

    internal class SbFreezeEnemies : SbContainerNode
    {
        private readonly CsEnemy[] _enemies;

        public SbFreezeEnemies(CsEnemy[] enemies, params SbNode[] children)
            : base(children)
        {
            _enemies = enemies;
        }

        public override void Build(CutsceneBuilder builder)
        {
            foreach (var e in _enemies)
            {
                builder.DisableEnemyCollision(e.Id);
                builder.HideEnemy(e.Id);
                builder.DeactivateEnemy(e.Id);
            }
            base.Build(builder);
            foreach (var e in _enemies)
            {
                builder.EnableEnemyCollision(e.Id);
                builder.UnhideEnemy(e.Id);
                builder.ActivateEnemy(e.Id);
            }
        }
    }

    internal class SbSetFlag : SbNode
    {
        public CsFlag Flag { get; }
        public bool Value { get; }

        public SbSetFlag(CsFlag flag, bool value = true)
        {
            Flag = flag;
            Value = value;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.SetFlag(Flag.Flag, Value);
        }
    }

    internal class SbProcedure : SbContainerNode
    {
        private static int g_nextId;
        private readonly int _id;

        public string Name { get; }

        public SbProcedure(params SbNode[] children)
            : base(children)
        {
            _id = Interlocked.Increment(ref g_nextId);
            Name = $"proc_{_id:x8}";
        }

        public SbProcedure(string name, params SbNode[] children)
            : base(children)
        {
            _id = Interlocked.Increment(ref g_nextId);
            Name = name;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.BeginProcedure(Name);
            base.Build(builder);
            builder.EndProcedure();
        }
    }

    internal class SbLoop : SbContainerNode
    {
        public SbLoop(params SbNode[] children)
            : base(children)
        {
        }

        public override void Build(CutsceneBuilder builder)
        {
            var repeatLabel = builder.CreateLabel();
            builder.AppendLabel(repeatLabel);
            base.Build(builder);
            builder.Goto(repeatLabel);
        }
    }

    internal class SbIf : SbNode
    {
        private readonly SbNode[] _if;
        private SbNode[]? _else = null;

        public CsFlag Flag { get; }
        public bool Value { get; }

        public SbIf(CsFlag flag, bool value, params SbNode[] children)
        {
            _if = children;
            Flag = flag;
            Value = value;
        }

        public SbIf Else(params SbNode[] children)
        {
            if (_else != null)
                throw new InvalidOperationException("Else already defined");

            _else = children;
            return this;
        }

        public override IEnumerable<SbNode> Children => _if.Concat(_else ?? new SbNode[0]);

        public override void Build(CutsceneBuilder builder)
        {
            builder.BeginIf();
            builder.CheckFlag(Flag.Flag, Value);
            foreach (var child in _if)
            {
                child.Build(builder);
            }
            if (_else != null)
            {
                builder.Else();
                foreach (var child in _else)
                {
                    child.Build(builder);
                }
            }
            builder.EndIf();
        }
    }

    internal class SbWaitForCut : SbNode
    {
        public int Cut { get; }

        public SbWaitForCut(int cut)
        {
            Cut = cut;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.WaitForTriggerCut(Cut);
        }
    }

    internal class SbWaitForFlag : SbNode
    {
        public ReFlag Flag { get; }
        public bool Value { get; }

        public SbWaitForFlag(ReFlag flag, bool value = true)
        {
            Flag = flag;
            Value = value;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.WaitForFlag(Flag, Value);
        }
    }

    internal class SbXaOn : SbNode
    {
        public int Value { get; }

        public SbXaOn(int value)
        {
            Value = value;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.PlayVoiceAsync(Value);
        }
    }

    internal class SbVoice : SbNode
    {
        public int Value { get; }

        public SbVoice(int value)
        {
            Value = value;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.PlayVoice(Value);
        }
    }

    internal interface ISbSubProcedure
    {
        public SbProcedure Procedure { get; }
    }

    internal class SbCall : SbNode, ISbSubProcedure
    {
        public SbProcedure Procedure { get; }

        public SbCall(SbProcedure procedure)
        {
            Procedure = procedure;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.Call(Procedure.Name);
        }
    }

    internal class SbFork : SbNode, ISbSubProcedure
    {
        public SbProcedure Procedure { get; }

        public SbFork(SbProcedure procedure)
        {
            Procedure = procedure;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.CallThread(Procedure.Name);
        }
    }

    internal class SbAot : SbNode
    {
        public CsAot Aot { get; }
        public REPosition Position { get; }
        public int Size { get; }

        public SbAot(CsAot aot, REPosition pos, int size)
        {
            Aot = aot;
            Position = pos;
            Size = size;
        }

        public override void Build(CutsceneBuilder builder)
        {
            var id = Aot.Id;
            var pos = Position;
            var size = Size;
            builder.AppendLine("aot_set", id, "SCE_AUTO", "SAT_PL | SAT_MANUAL | SAT_FRONT", pos.Floor, 0, pos.X - size / 2, pos.Z - size / 2, size, size, 0, 0, 0, 0, 0, 0);
        }
    }

    internal class SbEnableEvent : SbNode, ISbSubProcedure
    {
        public CsAot Aot { get; }
        public SbProcedure Procedure { get; }

        public SbEnableEvent(CsAot aot, SbProcedure procedure)
        {
            Aot = aot;
            Procedure = procedure;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.AppendLine("aot_reset", Aot.Id, "SCE_EVENT", "SAT_PL | SAT_MANUAL | SAT_FRONT", 255, 0, "I_GOSUB", Procedure.Name, 0, 0);
        }
    }

    internal class SbItem : SbNode
    {
        public CsItem Item { get; set; }

        public SbItem(CsItem item)
        {
            Item = item;
        }

        public override void Build(CutsceneBuilder builder)
        {
            builder.Item(Item.GlobalId, Item.Id, Item.Item.Type, Item.Item.Amount);
        }
    }
}
