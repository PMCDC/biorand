﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace rer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var re2Path = @"F:\games\re2";
            var originalDataPath = Path.Combine(re2Path, "data");
            var modPath = Path.Combine(re2Path, @"mod_rando");
            var gameData = GameDataReader.Read(originalDataPath, modPath);

            // DumpRdtList(gameData, @"M:\git\rer\docs\rdt.txt");
            // DumpScripts(gameData, @"F:\games\re2\mod_rando\scripts");
            return;

            var rng = new Rng();

            var numbers = new List<double>();
            for (int i = 0; i < 2048; i++)
                // numbers.Add((5 + rng.NextGaussian(-5, 1)) / 10);
                numbers.Add(rng.NextGaussian(-5, 2));
            numbers.Sort();

            // Generate(new RandoConfig()
            // {
            //     Seed = 0
            // });
        }

        public static void Generate(RandoConfig config, string re2Path)
        {
            config = config.Clone();
            if (config.GameVariant == 0)
            {
                // Leon A / Claire B
                // config.Player = 0;
                // config.Scenario = 0;
                // Generate2(config);

                config.Player = 1;
                config.Scenario = 1;
                Generate2(config, re2Path);
            }
            else
            {
                // Leon B / Claire A
                // config.Player = 0;
                // config.Scenario = 1;
                // Generate2(config);

                config.Player = 1;
                config.Scenario = 0;
                Generate2(config, re2Path);
            }
        }

        public static void Generate2(RandoConfig config, string re2Path)
        {
            var randomItems = new Rng(config.Seed);
            var randomNpcs = new Rng(config.Seed + 1);
            var randomEnemies = new Rng(config.Seed + 2);
            var randomMusic = new Rng(config.Seed + 3);

            var originalDataPath = Path.Combine(re2Path, "data");
            var modPath = Path.Combine(re2Path, @"mod_rando");

            if (Directory.Exists(modPath))
            {
                Directory.Delete(modPath, true);
            }
            Directory.CreateDirectory(modPath);

            using var logger = new RandoLogger(Path.Combine(modPath, $"log_pl{config.Player}.txt"));
            logger.WriteHeading("Resident Evil Randomizer");
            logger.WriteLine($"Seed: {config}");
            logger.WriteLine($"Player: {config.Player} {GetPlayerName(config.Player)}");
            logger.WriteLine($"Scenario: {GetScenarioName(config.Scenario)}");

            var map = LoadJsonMap(@"M:\git\rer\rer\data\rdt.json");
            var gameData = GameDataReader.Read(originalDataPath, modPath);



            if (config.RandomItems)
            {
                var factory = new PlayGraphFactory(logger, config, gameData, map, randomItems);
                // CheckRoomItems(gameData);
                // factory.CreateDoorRando();
                factory.Create();
                factory.Save();
            }

            if (config.RandomNPCs)
            {
                var npcRandomiser = new NPCRandomiser(logger, originalDataPath, modPath, gameData, map, randomNpcs);
                npcRandomiser.Randomise();
            }

            if (config.RandomEnemies)
            {
                var enemyRandomiser = new EnemyRandomiser(logger, config, gameData, randomEnemies);
                enemyRandomiser.Randomise();
            }

            if (config.RandomBgm)
            {
                var bgmRandomiser = new BgmRandomiser(logger, originalDataPath, modPath);
                bgmRandomiser.Randomise(randomMusic);
            }

            File.WriteAllText(Path.Combine(modPath, "manifest.txt"), "[MOD]\nName = BIOHAZARD 2: RANDOMIZER\n");
        }

        private static string GetPlayerName(int player) => player == 0 ? "Leon" : "Claire";
        private static string GetScenarioName(int scenario) => scenario == 0 ? "A" : "B";

        private static Map LoadJsonMap(string path)
        {
            var jsonMap = File.ReadAllText(path);
            var map = JsonSerializer.Deserialize<Map>(jsonMap, new JsonSerializerOptions()
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })!;
            return map;
        }

        private static void DumpRdtList(GameData gameData, string path)
        {
            var sb = new StringBuilder();
            foreach (var rdt in gameData.Rdts)
            {
                sb.AppendLine($"{rdt.RdtId} ():");
                foreach (var door in rdt.Doors)
                {
                    sb.AppendLine($"    Door #{door.Id}: {new RdtId(door.Stage, door.Room)} (0x{door.Offset:X2})");
                }
                foreach (var item in rdt.Items)
                {
                    sb.AppendLine($"    Item #{item.Id}: {(ItemType)item.Type} x{item.Amount} (0x{item.Offset:X2})");
                }
                foreach (var enemy in rdt.Enemies)
                {
                    sb.AppendLine($"    Enemy #{enemy.Id}: {enemy.Type} (0x{enemy.Offset:X2})");
                }
            }
            File.WriteAllText(path, sb.ToString());
        }

        private static void DumpScripts(GameData gameData, string scriptPath)
        {
            Directory.CreateDirectory(scriptPath);
            foreach (var rdt in gameData.Rdts)
            {
                var script = rdt.Script;
                File.WriteAllText(Path.Combine(scriptPath, $"{rdt.RdtId}.bio.c"), script);
            }
        }
    }
}
