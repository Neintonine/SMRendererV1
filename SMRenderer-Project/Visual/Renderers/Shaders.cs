﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Visual.Renderers
{
    internal class Shaders
    {
        public static Assembly ass = typeof(Shaders).Assembly;

        public static Dictionary<string, string> ShaderSource = new Dictionary<string, string>
        {
            {"general_vertex", Read("Shaders.general.main.vert", ass)},
            {"general_frag", Read("Shaders.general.main.frag", ass)},

            {"bloom_vertex", Read("Shaders.bloom.main.vert", ass)},
            {"bloom_frag", Read("Shaders.bloom.main.frag", ass)},

            {"particle_vertex", Read("Shaders.particle.main.vert", ass)},
            {"particle_frag", Read("Shaders.particle.main.frag", ass)},

            {"ext:bloom", Read("Shaders.ext.bloom.frag", ass)},
            {"ext:texture", Read("Shaders.ext.texturing.frag", ass)},
            {"ext:lighting", Read("Shaders.ext.lighting.frag", ass)},
            {"ext:funcs", Read("Shaders.ext.funcs.glsl", ass)},
            {"ext:depth", Read("Shaders.ext.depth.frag", ass) }
        };


        public static Dictionary<Type, ShaderProgramFiles> Storage = new Dictionary<Type, ShaderProgramFiles>
        {
            {
                typeof(GeneralRenderer), new ShaderProgramFiles
                {
                    vertex = new ShaderProgramFragment(ShaderSource["general_vertex"], ShaderType.VertexShader),
                    fragment = new ShaderProgramFragment(ShaderSource["general_frag"], ShaderType.FragmentShader)
                    {
                        "ext:bloom",
                        "ext:texture",
                        "ext:lighting",
                        "ext:depth"
                    }
                }
            },

            {
                typeof(BloomRenderer), new ShaderProgramFiles
                {
                    vertex = new ShaderProgramFragment(ShaderSource["bloom_vertex"], ShaderType.VertexShader),
                    fragment = new ShaderProgramFragment(ShaderSource["bloom_frag"], ShaderType.FragmentShader)
                }
            },

            {
                typeof(ParticleRenderer), new ShaderProgramFiles
                {
                    vertex = new ShaderProgramFragment(ShaderSource["particle_vertex"], ShaderType.VertexShader),
                    fragment = new ShaderProgramFragment(ShaderSource["particle_frag"], ShaderType.FragmentShader)
                    {
                        "ext:bloom",
                        "ext:texture",
                        "ext:lighting",
                        "ext:funcs",
                        "ext:depth"
                    }
                }
            }
        };

        /// <summary>
        ///     Reads the file contents
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns></returns>
        public static string Read(string path)
        {
            Stream stream = File.OpenRead(path);
            return new StreamReader(stream).ReadToEnd();
        }

        /// <summary>
        ///     Reads a file from a assembly
        /// </summary>
        /// <param name="path">The Path to file</param>
        /// <param name="assem">The Assembly</param>
        /// <returns></returns>
        public static string Read(string path, Assembly assem)
        {
            return new StreamReader(assem.GetManifestResourceStream(assem.GetName().Name + "." + path) ??
                                    throw new InvalidOperationException()).ReadToEnd();
        }
    }
}