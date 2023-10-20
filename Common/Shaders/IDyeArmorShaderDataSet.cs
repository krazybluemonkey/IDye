using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace IDye.Common.Shaders
{
    public class IDyeArmorShaderDataSet : ArmorShaderDataSet

    {
        internal List<ArmorShaderData> _shaderData = new List<ArmorShaderData>();

        internal Dictionary<int, int> _shaderLookupDictionary = new Dictionary<int, int>();

        internal int _shaderDataCount;

        public new T BindShader<T>(int itemId, T shaderData) where T : ArmorShaderData
        {
            _shaderLookupDictionary[itemId] = ++_shaderDataCount;
            _shaderData.Add(shaderData);
            return shaderData;
        }

        public new void Apply(int shaderId, Entity entity, DrawData? drawData = null)
        {
            if (shaderId >= 1 && shaderId <= _shaderDataCount)
            {
                _shaderData[shaderId - 1].Apply(entity, drawData);
            }
            else
            {
                Main.pixelShader.CurrentTechnique.Passes[0].Apply();
            }
        }

        public new void ApplySecondary(int shaderId, Entity entity, DrawData? drawData = null)
        {
            if (shaderId >= 1 && shaderId <= _shaderDataCount)
            {
                _shaderData[shaderId - 1].GetSecondaryShader(entity).Apply(entity, drawData);
            }
            else
            {
                Main.pixelShader.CurrentTechnique.Passes[0].Apply();
            }
        }

        public new ArmorShaderData GetShaderFromItemId(int type)
        {
            if (_shaderLookupDictionary.ContainsKey(type))
            {
                return _shaderData[_shaderLookupDictionary[type] - 1];
            }

            return null;
        }

        public new int GetShaderIdFromItemId(int type)
        {
            if (_shaderLookupDictionary.ContainsKey(type))
            {
                return _shaderLookupDictionary[type];
            }

            return 0;
        }

        public new ArmorShaderData GetSecondaryShader(int id, Player player)
        {
            if (id != 0 && id <= _shaderDataCount && _shaderData[id - 1] != null)
            {
                return _shaderData[id - 1].GetSecondaryShader(player);
            }

            return null;
        }
    }
}