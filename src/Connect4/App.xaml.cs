using Connect4.DAL.DatabaseHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Connect4
{
    public partial class App : Application
    {
        public App()
        {
            DatabaseInitializer.Initialize();
        }
    }
}
