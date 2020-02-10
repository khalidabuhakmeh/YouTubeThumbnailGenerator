using System.IO;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace YouTubeThumbnailGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            using var logo = Image.Load("./Images/logo_bar.png");
            var fonts = new FontCollection().Install("./BebasNeue-Regular.ttf");

            // create font
            var font = new Font(fonts, 130);
            
            var thumbnails = new []
            {
                new { title = "Hypermedia Lite For ASP.NET Core HTTP APIs", image = "./images/bg_lady_escalator.png" },
                new { title = "Reading RSS Feeds With .NET Core", image = "./images/bg_ghost_mask.png" },
                new { title = "Automate Your Blog With GitHub Actions", image = "./images/bg_computer_programmer.png" }
            };
            
            // output directory
            var outputDirectory = Directory.CreateDirectory("./output");

            foreach (var info in thumbnails)
            {
                using var thumbnail = Image.Load(info.image);

                // let's add the logo
                thumbnail.Mutate(x => x
                    .DrawImage(logo, new Point(0, 0), opacity: 1f)
                );

                // let's add the text shadow
                thumbnail.Mutate(x => x.DrawText
                    (
                        new TextGraphicsOptions
                        {
                            Antialias = true,
                            WrapTextWidth = 900f,
                            HorizontalAlignment = HorizontalAlignment.Center,
                        },
                        info.title,
                        font,
                        Color.MediumPurple,
                        new Point(203, 203)
                    )
                );
                
                // let's add the text
                thumbnail.Mutate(x => x.DrawText
                    (
                        new TextGraphicsOptions
                        {
                            Antialias = true,
                            WrapTextWidth = 900f,
                            HorizontalAlignment = HorizontalAlignment.Center,
                        },
                        info.title,
                        font,
                        Color.White,
                        new Point(200, 200)
                    )
                );

                var output = Path.Combine(outputDirectory.FullName,  Path.GetFileName(info.image));
                thumbnail.Save(output);
            }
        }
    }
    
    
}