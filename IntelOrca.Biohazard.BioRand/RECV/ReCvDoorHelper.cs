﻿using System.Collections.Generic;
using IntelOrca.Biohazard.Script.Opcodes;

namespace IntelOrca.Biohazard.BioRand.RECV
{
    internal class ReCvDoorHelper : IDoorHelper
    {
        public byte[] GetReservedLockIds() => new byte[0];

        public void Begin(RandoConfig config, GameData gameData, Map map)
        {
            // Do not lose lighter when giving Rodrigo medicine
            Nop(gameData, RdtId.Parse("1000"), 0x18CD7A);
            Nop(gameData, RdtId.Parse("1000"), 0x18DB74);
            // Do not put Rodrigo's gift into special slot
            Nop(gameData, RdtId.Parse("1000"), 0x18CD74);
            Nop(gameData, RdtId.Parse("1000"), 0x18DB7C);

            // Force version of 102 and 103 where you can get briefcase and use medal
            Nop(gameData, RdtId.Parse("1010"), 0x3EF2C); // Force RDT1021 to load
            Nop(gameData, RdtId.Parse("1010"), 0x3EF38, 0x3EF4C); // Force RDT1021 to load
            Nop(gameData, RdtId.Parse("1010"), 0x3EF50, 0x3EF5A); // Force RDT1021 to load
            Nop(gameData, RdtId.Parse("1050"), 0x1DF2AA, 0x1DF2BE); // Force RDT1031 to load
            Nop(gameData, RdtId.Parse("1050"), 0x1DF2C2, 0x1DF2CC); // Force RDT1031 to load
            // OverrideDoor(gameData, RdtId.Parse("1030"), 1, RdtId.Parse("1021"), 1);

            // Force window cutscene on item interaction
            Nop(gameData, RdtId.Parse("1070"), 0x1819AE);

            // Delete Steve/Alfred cutscene
            Nop(gameData, RdtId.Parse("3050"), 0x15F288, 0x15F2DA);
            Nop(gameData, RdtId.Parse("3050"), 0x15EEDC, 0x15EEF6);

            // Softlock can occur if you enter 305 via ladder without picking up silver key
            Patch(gameData, RdtId.Parse("3060"), 0x70A10 + 6, 0x00);

            // Change condition for going into 4011 so that it happens straight after Alfred cutscene
            Patch(gameData, RdtId.Parse("4080"), 0x9F86C + 2, 0xC5);
            Patch(gameData, RdtId.Parse("40F0"), 0x7241C + 2, 0xC5);

            // Force Steve to appear at airport
            Nop(gameData, RdtId.Parse("5000"), 0x187778, 0x18777A);
            Nop(gameData, RdtId.Parse("5000"), 0x187784, 0x18779C);
        }

        private void OverrideDoor(GameData gameData, RdtId rdtId, int aotIndex, RdtId target, int exit)
        {
            var rrdt = gameData.GetRdt(rdtId);
            if (rrdt == null)
                return;

            var aotIndexB = (byte)aotIndex;
            var stage = (byte)target.Stage;
            var room = (byte)target.Room;
            var variant = (byte)(target.Variant ?? 0);
            var exitB = (byte)exit;
            var texture = (byte)2;
            var unk = (byte)0;

            rrdt.AdditionalFrameOpcodes.Add(new UnknownOpcode(0, 0x01, new byte[] { 0x1A }));
            rrdt.AdditionalFrameOpcodes.Add(new UnknownOpcode(0, 0x04, new byte[] { 0x0A, 0x17, 0x00, aotIndexB, 0x00 }));
            rrdt.AdditionalFrameOpcodes.Add(new UnknownOpcode(0, 0xB6, new byte[] { variant }));
            rrdt.AdditionalFrameOpcodes.Add(new UnknownOpcode(0, 0x37, new byte[] { variant }));
            rrdt.AdditionalFrameOpcodes.Add(new UnknownOpcode(0, 0x33, new byte[] { 0x00, unk, 0x00, stage, room, exitB, texture }));
            rrdt.AdditionalFrameOpcodes.Add(new UnknownOpcode(0, 0x05, new byte[] { 0x0A, 0x17, 0x00, aotIndexB, 0x01 }));
            rrdt.AdditionalFrameOpcodes.Add(new UnknownOpcode(0, 0x03, new byte[] { 0x00 }));
        }

        private void Nop(GameData gameData, RdtId rtdId, int offset)
        {
            var rrdt = gameData.GetRdt(rtdId);
            if (rrdt == null)
                return;

            rrdt.Nop(offset);
        }

        private void Nop(GameData gameData, RdtId rtdId, int beginOffset, int endOffset)
        {
            var rrdt = gameData.GetRdt(rtdId);
            if (rrdt == null)
                return;

            rrdt.Nop(beginOffset, endOffset);
        }

        public void Patch(GameData gameData, RdtId rtdId, int offset, byte value)
        {
            var rrdt = gameData.GetRdt(rtdId);
            if (rrdt == null)
                return;

            rrdt.Patches.Add(new KeyValuePair<int, byte>(offset, value));
        }

        public void End(RandoConfig config, GameData gameData, Map map)
        {
        }
    }
}
