using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace WPFPart.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            randomImageClick = new RelayCommand(async () => {
                using (var client = new HttpClient())
                {
                    var stream = await client.GetStreamAsync("https://picsum.photos/500");
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();
                    ImageElement = image;
                }
            });

            saveImageClick = new RelayCommand(async () => {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Filter = "JPEG(*.jpeg)|*.jpeg|PNG(*.png)|*.png|BMP(*.bmp)|*.bmp|TIFF(*.tiff)|*.tiff";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(fileDialog.FileName))
                    {
                        var fileStream = fileDialog.OpenFile();
                        Bitmap image;
                        using (MemoryStream outStream = new MemoryStream() )
                        {
                            BitmapEncoder enc = new BmpBitmapEncoder();
                            enc.Frames.Add(BitmapFrame.Create(ImageElement));
                            enc.Save(outStream);
                            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                            image = new Bitmap(bitmap);
                        }
                        System.Drawing.Imaging.ImageFormat imageType = System.Drawing.Imaging.ImageFormat.Jpeg;
                        switch (fileDialog.FilterIndex)
                        {
                            case 1:
                                imageType = System.Drawing.Imaging.ImageFormat.Jpeg;
                                break;
                            case 2:
                                imageType = System.Drawing.Imaging.ImageFormat.Png;
                                break;                                            
                            case 3:
                                imageType = System.Drawing.Imaging.ImageFormat.Bmp;
                                break;                                            
                            case 4:                                               
                                imageType = System.Drawing.Imaging.ImageFormat.Tiff;
                                break;
                            default:
                                break;
                        }
                        await Task.Factory.StartNew(() => image.Save(fileStream, imageType));
                        fileStream.Close();
                        MessageBox.Show("Plik zapisany");
                    }
                }
            });

        }

        private string _imagePath { get; set; }
        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                var oldValue = _imagePath;
                _imagePath = value;
                RaisePropertyChanged(nameof(ImagePath), oldValue, value);
                Task.Factory.StartNew(() => ImageElement = new BitmapImage(new Uri(ImagePath)));
               
            }
        }

        private BitmapImage _imageElement { get; set; }
        public BitmapImage ImageElement
        {
            get
            {
                return _imageElement;
            }
            set
            {
                var oldValue = _imageElement;
                _imageElement = value;
                RaisePropertyChanged(nameof(ImageElement), oldValue, value);
            }
        }

        public RelayCommand randomImageClick { get; private set; }

        public RelayCommand saveImageClick { get; private set; }

    }
}