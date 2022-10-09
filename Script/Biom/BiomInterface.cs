using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// custom class using
using BuildingBeyondTheSpace;
using SettingsBeyondTheSpace;
using GridTileBeyondTheSpace;
using EnvironmentBeyondTheSpace;

namespace BiomBeyondTheSpace
{
    /// <Summary> 
    /// List of Function a Biom must have.<br/>
    /// </Summary>
    public interface BiomInterface
    {
        /// <Summary> 
        /// Get all Environements for a tile.<br/>
        /// </Summary>
        /// <param name="gridTile">the gridTile.</param>
        /// <param name="terrainSetting">the planet terrain settings.</param>
        public List<Environment> GetEnvironments(GridTile gridTile, TerrainPlanetGenerationSetting terrainSetting);
        /// <Summary> 
        /// Get all Buildings for a tile.<br/>
        /// </Summary>
        /// <param name="gridTile">the gridTile.</param>
        /// <param name="terrainSetting">the planet terrain settings.</param>
        public List<Building> GetBuildings(GridTile gridTile, TerrainPlanetGenerationSetting terrainSetting);
    }

}
