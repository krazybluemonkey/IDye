using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;

namespace IDye.Common.Shaders
{
    public class IDyeIntelligentShaderData : ShaderData
    {
        private Ref<Effect> _shader;

        private string _passName;

        private EffectPass _effectPass;

        public IDyeIntelligentShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
        {
        }

        public new Effect Shader
        {
            get
            {
                if (_shader != null)
                {
                    return _shader.Value;
                }

                return null;
            }
        }

        public void ShaderData(Ref<Effect> shader, string passName)
        {
            _passName = passName;
            _shader = shader;
        }

        public new void SwapProgram(string passName)
        {
            _passName = passName;
            if (passName != null)
            {
                _effectPass = Shader.CurrentTechnique.Passes[passName];
            }
        }

        public void SwapShader(Ref<Effect> shader)
        {
            _shader = shader;
        }

        public new void Apply()
        {
            if (_shader != null && Shader != null && _passName != null)
            {
                _effectPass = Shader.CurrentTechnique.Passes[_passName];
            }

            _effectPass.Apply();
        }
    }
}