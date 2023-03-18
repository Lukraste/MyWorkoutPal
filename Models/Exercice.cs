using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWorkoutPal.Models
{
    public class Exercice : INotifyPropertyChanged
    {
        int _id;
        public int Id 
        {
            get => _id;
            set 
            {
                if (_id == value)
                    return;

                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            } 
        }

        string _name;
        public string Name 
        {
            get => _name; 
            set
            {
                if (_name == value)
                    return;

                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            } 
        }
         string _description;
        public string Description
        {
            get => _description; 
            set
            {
                if (_description == value)
                    return;

                _description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            } 
        }
        string _muscle;
        public string Muscle
        {
            get => _muscle; 
            set
            {
                if (_muscle == value)
                    return;

                _muscle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Muscle)));
            } 
        }
        string _difficulty;
        public string Difficulty
        {
            get => _difficulty; 
            set
            {
                if (_difficulty == value)
                    return;

                _difficulty= value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Difficulty)));
            } 
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
