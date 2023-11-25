# Issue #1

System.IndexOutOfRangeException: Index was outside the bounds of the array.
   at AmmoboxPlus.AmmoboxHelpfulMethods.blastArea(Vector2 position, Int32 maxRange, Boolean extraDrops)
   at AmmoboxPlus.Projectiles.RocketMiner.Kill(Int32 timeLeft)
   at Terraria.ModLoader.ProjectileLoader.Kill_Obsolete(Projectile projectile, Int32 timeLeft)
   at Terraria.ModLoader.ProjectileLoader.OnKill(Projectile projectile, Int32 timeLeft)
   at Terraria.Projectile.Kill()
   at Terraria.Projectile.Update(Int32 i)
   at Terraria.Main.DoUpdateInWorld(Stopwatch sw)
   at Terraria.Main.DoUpdate(GameTime& gameTime)
   at Terraria.Main.Update(GameTime gameTime)
   at Microsoft.Xna.Framework.Game.Tick()
   at Microsoft.Xna.Framework.Game.RunLoop()
   at Microsoft.Xna.Framework.Game.Run()
   at Terraria.Program.RunGame()
   at Terraria.Program.LaunchGame_(Boolean isServer)
   at Terraria.Program.LaunchGame(String[] args, Boolean monoArgs)
   at Terraria.MonoLaunch.Main_End(String[] args)
   at Terraria.MonoLaunch.<>c__DisplayClass1_0.<Main>b__1()
   at Terraria.MonoLaunch.Main(String[] args)

# Issue #2

Arrows hitting floor don't have particles and wrong sound compared to vanilla.
