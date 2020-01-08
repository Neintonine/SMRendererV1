﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace SMRenderer.Renderers
{
    class DefaultShaders
    {
        public static Assembly ass = typeof(DefaultShaders).Assembly;

        public static string NormalVertex = Read("Shaders.shader_vertex.glsl", ass);
        public static string NormalFragment = Read("Shaders.shader_fragment.glsl", ass);

        public static string BloomVertex = Read("Shaders.shader_bloom_vertex.glsl", ass);
        public static string BloomFragment = Read("Shaders.shader_bloom_fragment.glsl", ass);

        public static string ParticleVertex = Read("Shaders.shader_particle_vertex.glsl", ass);
        public static string ParticleFragment = Read("Shaders.shader_particle_fragment.glsl", ass);
            
        /// <summary>
        /// Reads the file contents
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns></returns>
        public static string Read(string path)
        {
            Stream stream = File.OpenRead(path);
            return new StreamReader(stream).ReadToEnd();
        }
        /// <summary>
        /// Reads a file from a assembly
        /// </summary>
        /// <param name="path">The Path to file</param>
        /// <param name="assem">The Assembly</param>
        /// <returns></returns>
        public static string Read(string path, Assembly assem) {
            return new StreamReader(assem.GetManifestResourceStream(assem.GetName().Name + "." + path)).ReadToEnd();
        }
    }
}
