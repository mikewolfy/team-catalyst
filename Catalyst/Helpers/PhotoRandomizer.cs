﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalyst.Helpers
{
    public static class PhotoRandomizer
    {
        public static string GetRandomJuanImage()
        {
            var imageNames = new List<string>() {
                "juan.interns.hawaii.png",
                "juan.png",
                "juan.max.michael.png"
            };
            var randomIndex = new Random().Next(0, imageNames.Count() - 1);
            return imageNames[randomIndex];
        }
    }
}