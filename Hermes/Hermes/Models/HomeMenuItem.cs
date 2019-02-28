﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hermes.Models
{
    public enum MenuItemType
    {
        Chat,
        About,
        News,
        Maps
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
