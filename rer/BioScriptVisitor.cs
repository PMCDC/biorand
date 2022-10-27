﻿using System;
using System.IO;
using rer.Opcodes;

namespace rer
{
    internal class BioScriptVisitor
    {
        public virtual void VisitBeginScript(BioScriptKind kind)
        {
        }

        public virtual void VisitEndScript(BioScriptKind kind)
        {
        }

        public virtual void VisitBeginSubroutine(int index)
        {
        }

        public virtual void VisitEndSubroutine(int index)
        {
        }

        public virtual void VisitOpcode(int offset, Opcode opcode, Span<byte> operands) => VisitOpcode(offset, 1 + operands.Length, opcode, new BinaryReader(new MemoryStream(operands.ToArray())));

        private void VisitOpcode(int offset, int length, Opcode opcode, BinaryReader br)
        {
            switch (opcode)
            {
                default:
                    VisitUnknownOpcode(UnknownOpcode.Read(br, opcode, offset, length));
                    break;
                case Opcode.ElseCk:
                    VisitElseCk(ElseCkOpcode.Read(br, offset));
                    break;
                case Opcode.Gosub:
                    VisitGosub(GosubOpcode.Read(br, offset));
                    break;
                case Opcode.Ck:
                    VisitCk(CkOpcode.Read(br, offset));
                    break;
                case Opcode.DoorAotSe:
                    VisitDoorAotSe(DoorAotSeOpcode.Read(br, offset));
                    break;
                case Opcode.SceEmSet:
                    VisitSceEmSet(SceEmSetOpcode.Read(br, offset));
                    break;
                case Opcode.AotReset:
                    VisitAotReset(AotResetOpcode.Read(br, offset));
                    break;
                case Opcode.ItemAotSet:
                    VisitItemAotSet(ItemAotSetOpcode.Read(br, offset));
                    break;
                case Opcode.XaOn:
                    VisitXaOn(XaOnOpcode.Read(br, offset));
                    break;
                case Opcode.SceItemGet:
                    VisitSceItemGet(SceItemGetOpcode.Read(br, offset));
                    break;
            }
        }

        protected virtual void VisitOpcode(OpcodeBase opcode)
        {
        }

        protected virtual void VisitUnknownOpcode(UnknownOpcode opcode) => VisitOpcode(opcode);
        protected virtual void VisitElseCk(ElseCkOpcode opcode) => VisitOpcode(opcode);
        protected virtual void VisitGosub(GosubOpcode opcode) => VisitOpcode(opcode);
        protected virtual void VisitCk(CkOpcode opcode) => VisitOpcode(opcode);
        protected virtual void VisitDoorAotSe(DoorAotSeOpcode opcode) => VisitOpcode(opcode);
        protected virtual void VisitSceEmSet(SceEmSetOpcode opcode) => VisitOpcode(opcode);
        protected virtual void VisitAotReset(AotResetOpcode opcode) => VisitOpcode(opcode);
        protected virtual void VisitItemAotSet(ItemAotSetOpcode opcode) => VisitOpcode(opcode);
        protected virtual void VisitXaOn(XaOnOpcode opcode) => VisitOpcode(opcode);
        protected virtual void VisitSceItemGet(SceItemGetOpcode opcode) => VisitOpcode(opcode);
    }

    internal enum BioScriptKind
    {
        Init,
        Main
    }
}
