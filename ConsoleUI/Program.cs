using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //InMemoryDemo();
            //EntityFrameworkDemo();

            var now = DateTime.Now;

            var nextTime = new DateTime(2021, 12, 30);

            if (nextTime > now)
            {
                Console.WriteLine("nextTime büyük");
            }
            else
            {
                Console.WriteLine("now büyük");
            }


            Console.ReadLine();
        }

        private static void EntityFrameworkDemo()
        {
            BrandEntityFrameworkDemo();
            ColorEntityFrameworkDemo();
            CarEntityFrameworkDemo();
        }

        private static void CarGetCarDetailsDemo(CarManager carManager)
        {
            var cars = carManager.GetCarDetails();
            foreach (var car in cars.Data)
            {
                Console.WriteLine(
                    car.BrandName + " - " +
                    car.ColorName + " - " +
                    car.DailyPrice + " - " +
                    car.ModelYear + " - " +
                    car.Description);
            }
        }

        private static void InMemoryDemo()
        {
            CarInMemoryDemo();
        }

        private static void CarEntityFrameworkDemo()
        {
            CarManager carManager = new CarManager(new EfCarDal(),new BrandManager(new EfBrandDal()));

            CarGetAllDemo(carManager);
            CarGetByIdDemo(carManager);
            CarAddDemo(carManager);
            CarUpdateDemo(carManager);
            CarDeleteDemo(carManager);
            CarGetCarDetailsDemo(carManager);
        }

        private static void CarDeleteDemo(CarManager carManager)
        {
            carManager.Delete(new Car
            {
                Id = 13
            });

            CarGetAllDemo(carManager);
        }

        private static void CarUpdateDemo(CarManager carManager)
        {
            carManager.Update(new Car
            {
                Id = 6,
                BrandId = 3,
                ColorId = 3,
                ModelYear = 2020,
                DailyPrice = 280,
                Description = "merco-gümüş",
                Model = "Model"
            });

            CarGetAllDemo(carManager);
        }

        private static void CarGetByIdDemo(CarManager carManager)
        {
            var car = carManager.GetById(8);
            Console.WriteLine(
                    car.Data.Id + " - " +
                    car.Data.Description + " - " +
                    car.Data.ModelYear + " - " +
                    car.Data.DailyPrice + " - " +
                    car.Data.Model);
        }

        private static void CarAddDemo(CarManager carManager)
        {
            carManager.Add(new Car
            {
                BrandId = 1,
                ColorId = 1,
                ModelYear = 2021,
                DailyPrice = 300,
                Description = "audi-siyah",
                Model = "Model"
            });
            carManager.Add(new Car
            {
                BrandId = 3,
                ColorId = 4,
                ModelYear = 2019,
                DailyPrice = 200,
                Description = "merco-kırmızı",
                Model = "Model"
            });
            carManager.Add(new Car
            {
                BrandId = 4,
                ColorId = 3,
                ModelYear = 2020,
                DailyPrice = 250,
                Description = "ford-gümüş",
                Model = "Model"
            });
            carManager.Add(new Car
            {
                BrandId = 5,
                ColorId = 4,
                ModelYear = 2018,
                DailyPrice = 150,
                Description = "fiat-kırmızı",
                Model = "Model"
            });
            carManager.Add(new Car
            {
                BrandId = 4,
                ColorId = 4,
                ModelYear = 2021,
                DailyPrice = 290,
                Description = "ford-kırmızı",
                Model = "Model"
            });
            carManager.Add(new Car
            {
                BrandId = 3,
                ColorId = 5,
                ModelYear = 2021,
                DailyPrice = 320,
                Description = "merco-yeşil",
                Model = "Model"
            });
            carManager.Add(new Car
            {
                BrandId = 2,
                ColorId = 7,
                ModelYear = 2017,
                DailyPrice = 220,
                Description = "bmw-lacivert",
                Model = "Model"
            });
            carManager.Add(new Car
            {
                BrandId = 2,
                ColorId = 2,
                ModelYear = 2021,
                DailyPrice = 360,
                Description = "bmw-beyaz",
                Model = "Model"
            });
            carManager.Add(new Car
            {
                BrandId = 3,
                ColorId = 6,
                ModelYear = 2021,
                DailyPrice = 355,
                Description = "merco-mavi",
                Model = "Model"
            });
            carManager.Add(new Car
            {
                BrandId = 7,
                ColorId = 6,
                ModelYear = 2018,
                DailyPrice = 200,
                Description = "volvo-mavi",
                Model = "Model"
            });
            carManager.Add(new Car
            {
                BrandId = 7,
                ColorId = 6,
                ModelYear = 2021,
                DailyPrice = 330,
                Description = "volvo-mavi",
                Model = "Model"
            });
            carManager.Add(new Car
            {
                BrandId = 6,
                ColorId = 2,
                ModelYear = 2021,
                DailyPrice = 310,
                Description = "reno-beyaz",
                Model = "Model"
            });
            carManager.Add(new Car
            {
                BrandId = 3,
                ColorId = 1,
                ModelYear = 2020,
                DailyPrice = 360,
                Description = "merco-siyah",
                Model = "Model"
            });

            CarGetAllDemo(carManager);
        }

        private static void CarGetAllDemo(CarManager carManager)
        {
            var cars = carManager.GetAll();
            foreach (var car in cars.Data)
            {
                Console.WriteLine(
                    car.Id + " - " +
                    car.Description + " - " +
                    car.ModelYear + " - " +
                    car.Model + " - " +
                    car.DailyPrice);
            }
        }

        private static void ColorEntityFrameworkDemo()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());

            ColorGetAllDemo(colorManager);
            ColorGetByIdDemo(colorManager);
            ColorAddDemo(colorManager);
            ColorUpdateDemo(colorManager);
            ColorDeleteDemo(colorManager);
        }

        private static void ColorDeleteDemo(ColorManager colorManager)
        {
            colorManager.Delete(new Color
            {
                Id = 9
            });

            ColorGetAllDemo(colorManager);
        }

        private static void ColorGetByIdDemo(ColorManager colorManager)
        {
            var color = colorManager.GetById(2);
            Console.WriteLine(color.Data.Id + " - " + color.Data.Name);
        }

        private static void ColorUpdateDemo(ColorManager colorManager)
        {
            colorManager.Update(new Color
            {
                Id = 5,
                Name = "Açık yeşil"
            });

            ColorGetAllDemo(colorManager);
        }

        private static void ColorAddDemo(ColorManager colorManager)
        {
            colorManager.Add(new Color
            {
                Name = "Siyah"
            });
            colorManager.Add(new Color
            {
                Name = "Beyaz"
            });
            colorManager.Add(new Color
            {
                Name = "Gümüş"
            });
            colorManager.Add(new Color
            {
                Name = "Kırmızı"
            });
            colorManager.Add(new Color
            {
                Name = "Yeşil"
            });
            colorManager.Add(new Color
            {
                Name = "Mavi"
            });
            colorManager.Add(new Color
            {
                Name = "Lacivert"
            });
            colorManager.Add(new Color
            {
                Name = "Bordo"
            });
            colorManager.Add(new Color
            {
                Name = "Sarı"
            });
            ColorGetAllDemo(colorManager);
        }

        private static void ColorGetAllDemo(ColorManager colorManager)
        {
            var colors = colorManager.GetAll();
            foreach (var color in colors.Data)
            {
                Console.WriteLine(color.Id + " - " + color.Name);
            }
        }

        private static void BrandEntityFrameworkDemo()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            BrandGetAllDemo(brandManager);
            BrandGetByIdDemo(brandManager);
            BrandAddDemo(brandManager);
            BrandUpdateDemo(brandManager);
            BrandDeleteDemo(brandManager);
        }

        private static void BrandGetByIdDemo(BrandManager brandManager)
        {
            var brand = brandManager.GetById(5);
            Console.WriteLine(brand.Data.Id + " - " + brand.Data.Name);
        }

        private static void BrandDeleteDemo(BrandManager brandManager)
        {
            brandManager.Delete(new Brand
            {
                Id = 8
            });
            BrandGetAllDemo(brandManager);
        }

        private static void BrandUpdateDemo(BrandManager brandManager)
        {
            brandManager.Update(new Brand
            {
                Id = 3,
                Name = "Mercedes-Benz"
            });
            BrandGetAllDemo(brandManager);
        }

        private static void BrandAddDemo(BrandManager brandManager)
        {
            brandManager.Add(new Brand
            {
                Name = "Audi"
            });
            brandManager.Add(new Brand
            {
                Name = "BMW"
            });
            brandManager.Add(new Brand
            {
                Name = "Mercedes"
            });
            brandManager.Add(new Brand
            {
                Name = "Ford"
            });
            brandManager.Add(new Brand
            {
                Name = "Fiat"
            });
            brandManager.Add(new Brand
            {
                Name = "Renault"
            });
            brandManager.Add(new Brand
            {
                Name = "Volvo"
            });
            brandManager.Add(new Brand
            {
                Name = "Mazda"
            });
            BrandGetAllDemo(brandManager);
        }

        private static void BrandGetAllDemo(BrandManager brandManager)
        {
            var brands = brandManager.GetAll();
            foreach (var brand in brands.Data)
            {
                Console.WriteLine(brand.Id + " - " + brand.Name);
            }
        }

        private static void CarInMemoryDemo()
        {
            CarManager carManager = new CarManager(new ImCarDal(),new BrandManager(new EfBrandDal()));

            CarGetAllDemoIm(carManager);
            CarGetByIdDemoIm(carManager);
            CarAddDemoIm(carManager);
            CarUpdateDemoIm(carManager);
            CarDeleteDemoIm(carManager);
            CarGetCarsByBrandIdIm(carManager);
            CarGetCarsByColorIdIm(carManager);
            CarAddBusinessRulesDemoIm(carManager);
        }

        private static void CarAddBusinessRulesDemoIm(CarManager carManager)
        {
            carManager.Add(new Car
            {
                Id = 6,
                BrandId = 3,
                ColorId = 2,
                ModelYear = 2019,
                DailyPrice = 0,
                Model = "Model",
                Description = "A"
            });
        }


        private static void CarGetCarsByColorIdIm(CarManager carManager)
        {
            var result = carManager.GetCarsByColorId(2);
            foreach (var car in result.Data)
            {
                Console.WriteLine(
                    car.Id + " - " +
                    car.BrandId + " - " +
                    car.ColorId + " - " +
                    car.DailyPrice + " - " +
                    car.ModelYear + " - " +
                    car.Model + " - " +
                    car.Description);
            }
        }

        private static void CarGetCarsByBrandIdIm(CarManager carManager)
        {
            var result = carManager.GetCarsByBrandId(3);
            foreach (var car in result.Data)
            {
                Console.WriteLine(
                    car.Id + " - " +
                    car.BrandId + " - " +
                    car.ColorId + " - " +
                    car.DailyPrice + " - " +
                    car.ModelYear + " - " +
                    car.Model + " - " +
                    car.Description);
            }
        }


        private static void CarDeleteDemoIm(CarManager carManager)
        {
            carManager.Delete(new Car
            {
                Id = 2
            });
            CarGetAllDemoIm(carManager);
        }

        private static void CarUpdateDemoIm(CarManager carManager)
        {
            carManager.Update(new Car
            {
                Id = 6,
                BrandId = 3,
                ColorId = 2,
                ModelYear = 2019,
                DailyPrice = 160,
                Model = "1.5 Icon",
                Description = "1.5 Icon"
            });
            CarGetAllDemoIm(carManager);
        }

        private static void CarAddDemoIm(CarManager carManager)
        {
            carManager.Add(new Car
            {
                BrandId = 5,
                ColorId = 3,
                ModelYear = 2019,
                DailyPrice = 160,
                Model = "Linea",
                Description = "Linea"
            });
            CarGetAllDemoIm(carManager);
        }

        private static void CarGetByIdDemoIm(CarManager carManager)
        {
            var result = carManager.GetById(5);

            Console.WriteLine(
                    result.Data.Id + " - " +
                    result.Data.BrandId + " - " +
                    result.Data.ColorId + " - " +
                    result.Data.DailyPrice + " - " +
                    result.Data.ModelYear + " - " +
                    result.Data.Model + " - " +
                    result.Data.Description);
        }

        private static void CarGetAllDemoIm(CarManager carManager)
        {
            var result = carManager.GetAll();
            foreach (var car in result.Data)
            {
                Console.WriteLine(
                    car.Id + " - " +
                    car.BrandId + " - " +
                    car.ColorId + " - " +
                    car.DailyPrice + " - " +
                    car.ModelYear + " - " +
                    car.Model + " - " +
                    car.Description);
            }
        }
    }
}
