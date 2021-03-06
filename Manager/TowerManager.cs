using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using TriangleGame.Objects.Enemys;
using TriangleGame.Resources;

namespace TriangleGame.Manager
{
    public class TowerManager
    {
        private List<Tower> _towers;
        private List<Connector> _connectors;
        private List<Area> _areas;

        private List<Ore> _ores;
        private List<Enemy> _enemies;

        private int _storage;

        private int _minDistance = 75;
        private int _maxDistance = 240;

        private bool _enemySpawn = false;

        public TowerManager()
        {
            _towers = new List<Tower>();
            _connectors = new List<Connector>();
            _areas = new List<Area>();
            _ores = new List<Ore>();
            _enemies = new List<Enemy>();
            _storage = 1000;
        }

        public List<Tower> Towers
        {
            get => _towers;
        }

        public List<Connector> Connectors
        {
            get => _connectors;
        }

        public List<Area> Areas
        {
            get => _areas;
        }

        public List<Ore> Ores
        {
            get => _ores;
        }

        public List<Enemy> Enemies
        {
            get => _enemies;
        }

        public bool SpawnEnemies
        {
            get => _enemySpawn;
            set => _enemySpawn = value;
        }

        public bool AddTower(Tower tower)
        {
            bool valid = true;
            foreach (var area in _areas)
            {
                if (area.Contains(tower))
                {
                    valid = false;
                }
            }

            bool check = true;
            foreach (var t in _towers)
            {
                float distance = Vector2.Distance(t.Position.ToVector2(), tower.Position.ToVector2());

                if (distance < _minDistance) //&& distance < _maxDistance
                {
                    check = false;
                }

                if (_towers.Count < 2)
                {
                    if (distance < _minDistance || distance > _maxDistance)
                    {
                        check = false;
                    }
                }
            }

            if (_towers.Count == 0) check = true;

            if (valid && check)
            {
                _towers.Add(tower);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GenerateOres(Rectangle area, Texture2D texture)
        {
            GenerateOres(area.Location, area.Location + area.Size, texture);
        }

        public List<Area> GetAreasOfTower(Tower tower)
        {
            List<Area> areas = new List<Area>();
            foreach (var area in _areas)
            {
                if (area.Contains(tower))
                {
                    areas.Add(area);
                }
            }

            return areas;
        }

        public List<Ore> GetUnoccupiedOresInArea(Area area, ResourceType preffered)
        {
            List<Ore> ores = new List<Ore>();
            foreach (var ore in _ores)
            {
                if (!area.Contains(ore)) continue;
                if (ore.Occupied) continue;
                if (preffered == ResourceType.None)
                {
                    ores.Add(ore);
                    return ores;
                }

                if (ore.Resource == preffered)
                {
                    ores.Add(ore);
                    return ores;
                }
            }

            return ores;
        }

        public void GenerateOres(Point start, Point end, Texture2D texture)
        {
            if (start.X > end.X || start.Y > end.Y)
            {
                throw new Exception("Start and End point are out of position");
            }

            var rand = new Random();
            for (var i = start.X; i < end.X; i++)
            {
                for (var j = start.Y; j < end.Y; j++)
                {
                    if (rand.Next() % 750 == 0)
                    {
                        int res = rand.Next() % 3;
                        ResourceType type = ResourceType.Metal;
                        switch (res)
                        {
                            case 0:
                                type = ResourceType.Metal;
                                break;
                            case 1:
                                type = ResourceType.Gas;
                                break;
                            case 2:
                                type = ResourceType.Crystals;
                                break;
                        }

                        _ores.Add(new Ore(new Point(i, j), new Point(6, 6), type, texture,
                            Color.LimeGreen, 200));
                    }
                }
            }
        }

        public void Connect(Tower t, bool startup = false)
        {
            bool invalid = false;
            int areas = 0;

            foreach (var tower1 in _towers.Where(p =>
                Vector2.Distance(t.Position.ToVector2(), p.Position.ToVector2()) < _maxDistance))
            {
                if (t == tower1) continue;

                float distance = Vector2.Distance(t.Position.ToVector2(), tower1.Position.ToVector2());

                if (distance < _minDistance || distance > _maxDistance) continue;

                var c1 = new Connector(t, tower1);
                bool intersect = false;

                foreach (var connector in _connectors.Where(connector => c1.Intersects(connector)))
                {
                    intersect = true;
                }

                if (intersect)
                {
                    Console.WriteLine("INTERSECTION CONTINUE");
                    continue;
                }

                _connectors.Add(c1);
                var dist = _connectors[_connectors.Count - 1].Length;

                for (var i = 0; i < _connectors.Count; i++)
                {
                    Tower t1 = c1.TowerA;
                    Tower t2 = c1.TowerB;
                    Tower t3 = null;

                    for (var j = i + 1; j < _connectors.Count; j++)
                    {
                        Connector c2 = _connectors[j];

                        bool found = false;
                        if (c2.Equals(c1)) continue;

                        if (t1.Equals(c2.TowerA))
                        {
                            t3 = c2.TowerB;
                            found = true;
                        }
                        else if (t1.Equals(c2.TowerB))
                        {
                            t3 = c2.TowerA;
                            found = true;
                        }

                        if (!found) continue;

                        foreach (var c3 in _connectors)
                        {
                            if (!c3.Equals(c1) && !c3.Equals(c2))
                            {
                                //Check das alle drei Tower unique sind
                                if ((!t3.Equals(c3.TowerA) || !t2.Equals(c3.TowerB)) &&
                                    (!t3.Equals(c3.TowerB) || !t2.Equals(c3.TowerA))) continue;

                                Vector2 a = t1.Position.ToVector2() - t2.Position.ToVector2();
                                Vector2 b = t3.Position.ToVector2() - t2.Position.ToVector2();
                                a.Normalize();
                                b.Normalize();

                                float ab = Vector2.Dot(a, b);

                                if (ab >= -0.85f && ab <= 0.85f)
                                {
                                    Area area = new Area(c1, c2, c3);

                                    _areas.Add(area);
                                    areas++;
                                }
                            }
                        }
                    }
                }
            }

            if (areas == 0 && !startup)
            {
                RemoveConnections(t);
                _towers.Remove(t);
            }

            if (invalid)
            {
                RemoveConnections(t);
                _towers.Remove(t);
            }
        }

        public void RemoveConnections(Tower t)
        {
            for (int i = _connectors.Count - 1; i >= 0; i--)
            {
                if (_connectors[i].Contains(t)) _connectors.Remove(_connectors[i]);
            }
        }

        public void RemoveAreas(Tower t)
        {
            for (int i = _areas.Count - 1; i >= 0; i--)
            {
                if (_areas[i].ContainsTower(t))
                {
                    _areas.Remove(_areas[i]);
                }
            }
        }

        public void ClearTower(Tower t)
        {
            RemoveAreas(t);
            RemoveConnections(t);
            _towers.Remove(t);
        }

        public void ClearAllTowers()
        {
            _areas.Clear();
            _connectors.Clear();
            _towers.Clear();
        }

        private void PlaceEnemy(Tower perimiter, int range)
        {
            Point maxDistance = new Point(perimiter.Position.X - range / 2, perimiter.Position.Y - range / 2);

            Random rand = new Random();

            Point spawnPosition;
            int counter = 20;

            do
            {
                int x = (rand.Next() % range) + maxDistance.X;
                int y = (rand.Next() % range) + maxDistance.Y;
                spawnPosition = new Point(x, y);
                counter--;
            } while (IsInArea(spawnPosition) && counter > 0);

            if (counter > 0)
            {
                _enemies.Add(new Enemy(spawnPosition, new Point(12, 12), TextureManager.Instance.Sprites["pixel"],
                    Color.Yellow, 50, 2, 2));
            }
        }

        private bool IsInArea(Point position)
        {
            foreach (var area in _areas)
            {
                if (area.Contains(position)) return true;
            }

            return false;
        }

        private Tower ClosestTower(Point position)
        {
            Tower closest = null;
            foreach (var tower in _towers)
            {
                if (closest == null)
                {
                    closest = tower;
                }
                else
                {
                    float distanceClosest = Vector2.Distance(position.ToVector2(), closest.Position.ToVector2());
                    float distanceNew = Vector2.Distance(position.ToVector2(), tower.Position.ToVector2());

                    if (distanceNew < distanceClosest)
                    {
                        closest = tower;
                    }
                }
            }

            return closest;
        }

        public Enemy ClosestEnemy(Point position)
        {
            Enemy closest = null;
            foreach (var enemy in _enemies)
            {
                if (closest == null)
                {
                    closest = enemy;
                }
                else
                {
                    float distanceClosest = Vector2.Distance(position.ToVector2(), closest.Position.ToVector2());
                    float distanceNew = Vector2.Distance(position.ToVector2(), enemy.Position.ToVector2());

                    if (distanceNew < distanceClosest)
                    {
                        closest = enemy;
                    }
                }
            }

            return closest;
        }

        /*private double Angle(Connector a, Connector b, Connector c)
        {
            double step =
                (Math.Pow(a.Length, 2) + Math.Pow(b.Length, 2) -
                 Math.Pow(c.Length, 2)) / (2 * a.Length * b.Length);
            return Math.Acos(step) * 180 / Math.PI;
        }

        public double SumArea()
        {
            double sum = 0;
            foreach (var area in _areas)
            {
                sum += area.Size();
            }

            return sum;
        }*/

        public void EnemyUpdate()
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                EnemyState state = _enemies[i].Action();
                if (state == EnemyState.SEARCH)
                {
                    _enemies[i].Select(ClosestTower(_enemies[i].Position));
                }
                else if (state == EnemyState.DEAD)
                {
                    _enemies.Remove(_enemies[i]);
                }
            }
        }

        public Dictionary<string, int> Update(MouseInfo mouse, bool miningInterval, bool attackInterval)
        {
            Dictionary<string, int> resource = new Dictionary<string, int>();
            foreach (var tower in _towers)
            {
                tower.HealthBar.Active = tower.HoverText.Update(mouse.RelativPosition);
                if (tower is CollectorTower ctower)
                {
                    ctower.UpdateUI(mouse);
                    if (miningInterval)
                    {
                        KeyValuePair<string, int> temp = new KeyValuePair<string, int>("none", -1);
                        temp = ctower.Update();
                        if (!temp.Key.Equals("none"))
                        {
                            string key = temp.Key;
                            int amount = temp.Value;
                            if (resource.ContainsKey(key))
                            {
                                resource[key] += amount;
                            }
                            else
                            {
                                resource.Add(key, amount);
                            }
                        }
                    }
                }
                else if (tower is AttackTower atower && attackInterval)
                {
                    if (atower.TargetSelected())
                    {
                        atower.Update();
                    }
                    else
                    {
                        atower.TargetEnemy(ClosestEnemy(atower.Position));
                    }
                }
            }

            if (miningInterval)
            {
                if (_enemySpawn && _towers.Count > 0)
                {
                    Random rand = new Random();
                    if (rand.Next() % 3 == 0)
                    {
                        int t = rand.Next() % _towers.Count;
                        PlaceEnemy(_towers[t], 500);
                    }
                }
            }

            return resource;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var atower in _towers)
            {
                if (atower is AttackTower a)
                {
                    a.DrawRange(spriteBatch);
                }
            }
            
            foreach (var ore in _ores)
            {
                ore.Draw(spriteBatch);
            }

            foreach (var ore in _ores)
            {
                ore.HoverText.Draw(spriteBatch);
            }

            foreach (var area in _areas)
            {
                area.Draw(spriteBatch);
            }

            foreach (var enemy in _enemies)
            {
                enemy.Draw(spriteBatch);
            }

            foreach (var tower in _towers)
            {
                if (tower is CollectorTower collectorTower)
                {
                    collectorTower.Draw(spriteBatch);
                }
                else
                {
                    tower.Draw(spriteBatch);
                }
            }

            foreach (var tower in _towers)
            {
                tower.HealthBar.Draw(spriteBatch);
                tower.HoverText.Draw(spriteBatch);
            }
            
            foreach (var enemy in _enemies)
            {
                enemy.HealthBar.Draw(spriteBatch);
            }
        }
    }
}