using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Apod
{
    public class ImageContext : INotifyPropertyChanged
    {
        /// <summary>
        /// The title of the image
        /// </summary>
        private string title;

        /// <summary>
        /// The status of the image
        /// </summary>
        private string status;

        /// <summary>
        /// The apod image
        /// </summary>
        private BitmapSource imageSource;

        ///// <summary>
        ///// The date of the picture
        ///// </summary>
        //private DateTime imageDate;

        /// <summary>
        /// The file name of the image
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// An indicator that lets you know if the image is valid or not
        /// </summary>
        public bool ValidImage { get; set; }

        /// <summary>
        /// Gets or sets the title of the image
        /// </summary>
        public string Title
        {
            get => this.title;
            set
            {
                if (value == this.title) return;
                this.title = value;
                this.OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Gets or sets the status of the image
        /// </summary>
        public string Status
        {
            get => this.status;
            set
            {
                if (value == this.status) return;
                this.status = value;
                this.OnPropertyChanged("Status");
            }
        }

        /// <summary>
        /// Gets or sets the image source
        /// </summary>
        public BitmapSource ImageSource
        {
            get => this.imageSource;
            set
            {
                if (Equals(value, this.imageSource)) return;
                this.imageSource = value;
                this.OnPropertyChanged("ImageSource");
            }
        }

        /// <summary>
        /// Gets or sets the image date
        /// </summary>
        //public DateTime ImageDate
        //{
        //    get => this.imageDate;
        //    set
        //    {
        //        if (value == this.imageDate) return;
        //        this.imageDate = value;
        //        this.OnPropertyChanged("ImageDate");
        //    }
        //}

        /// <summary>
        /// Declare the property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// A method that is used to raise a property changed event
        /// </summary>
        /// <param name="name">The name of the property</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
