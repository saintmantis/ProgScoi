using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Threading;

namespace ConsoleApp4
{
	class Program
	{
		#region // мой main
		static void Main()
		{
			Console.WriteLine("Введите имя первого файла: ");
			string nameImg1 = Console.ReadLine(); // имя первого файла
			Console.WriteLine("Введите имя второго файла: ");
			string nameImg2 = Console.ReadLine(); // имя второго файла
			using (var img1 = new Bitmap("..\\..\\" + nameImg1))
			{
				Console.WriteLine("Открываю изображение 1" + Directory.GetParent("..\\..\\") + "\\" + nameImg1);
				var widthImage1 = img1.Width;
				var heightImage1 = img1.Height;
				Console.WriteLine($"ширина изображения 1: {widthImage1} \nВысота ихзображения 1: {heightImage1}");
				using (var img2 = new Bitmap("..\\..\\" + nameImg2))
				{
					Console.WriteLine("Открываю изображение 2" + Directory.GetParent("..\\..\\") + "\\" + nameImg2);
					var widthImage2 = img2.Width;
					var heightImage2 = img2.Height;
					Console.WriteLine($"ширина изображения 2: {widthImage2} \nВысота ихзображения 2: {heightImage2}");
					menu(widthImage1, heightImage1, img1, img2);
				}
			}
			Console.ReadKey();
		}
		#endregion

		#region // мои функции

		public static void menu(int widthImage1, int heightImage1, Bitmap img1, Bitmap img2)
		{

			string flag = "r";
			while (flag == "r")
			{
				Console.BackgroundColor = ConsoleColor.Red;
				Console.WriteLine("                                     Меню                                                   ");
				Console.ResetColor();
				Console.BackgroundColor = ConsoleColor.Blue;
				Console.Write("Нажмите");
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.Write("'1'");
				Console.ResetColor();
				Console.BackgroundColor = ConsoleColor.Blue;
				Console.WriteLine("если хотите вычислить попиксельно сумму двух изображений                          ");
				//
				Console.Write("Нажмите");
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.Write("'2'");
				Console.ResetColor();
				Console.BackgroundColor = ConsoleColor.Blue;
				Console.WriteLine("если хотите вычислить пиксельное произведение двух изображений                    ");
				//
				Console.Write("Нажмите");
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.Write("'3'");
				Console.ResetColor();
				Console.BackgroundColor = ConsoleColor.Blue;
				Console.WriteLine("если хотите вычислить попиксельное среднее-арифмитическое двух изображений        ");
				//
				Console.Write("Нажмите");
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.Write("'4'");
				Console.ResetColor();
				Console.BackgroundColor = ConsoleColor.Blue;
				Console.WriteLine("если хотите вычислить пиксельный минимум двух изображений                         ");
				//
				Console.Write("Нажмите");
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.Write("'5'");
				Console.ResetColor();
				Console.BackgroundColor = ConsoleColor.Blue;
				Console.WriteLine("если хотите вычислить пиксельный максимум двух изображений                        ");
				//
				Console.Write("Нажмите");
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.Write("'6'");
				Console.ResetColor();
				Console.BackgroundColor = ConsoleColor.Blue;
				Console.WriteLine("если хотите наложить на изображение маску (круг, квадрат, прямоугольник)          ");
				//
				Console.Write("Для выхода наберите");
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine("'end'                                                                    ");
				Console.ResetColor();
				//
				int caseSwitch = Convert.ToInt32(Console.ReadLine());
				switch (caseSwitch)
				{
					case 1:
						pixelSumOfTwoImages(widthImage1, heightImage1, img1, img2); // сумма
						break;
					case 2:
						pixelProductOfTwoImages(widthImage1, heightImage1, img1, img2); // произведние
						break;
					case 3:
						pixelArithmeticMeanOfTwoImages(widthImage1, heightImage1, img1, img2); // среднее арифмитическое
						break;
					case 4:
						pixelMinOfTwoImages(widthImage1, heightImage1, img1, img2); // пиксельный минимум
						break;
					case 5:
						pixelMaxOfTwoImages(widthImage1, heightImage1, img1, img2); // пиксельный максимум
						break;
					case 6:
						applyMaskToImage(widthImage1, heightImage1, img1, img2); // маска
						break;
					default:
						Console.WriteLine("Ошибка ! Введите пункт меню а не что то другое!!!");
						break;
				}
				Thread.Sleep(2000);
				Console.Clear();
			}
		}
		public static void pixelSumOfTwoImages(int widthImage1, int heightImage1, Bitmap img1, Bitmap img2)
		{
			Stopwatch stopwatch1 = new Stopwatch();
			stopwatch1.Start();
			using (var img_out = new Bitmap(widthImage1, heightImage1))
			{

				for (int i = 0; i < img1.Height; i++)
				{
					for (int j = 0; j < img1.Width; j++)
					{
						//читаем пиксель первой картинки
						var pix1 = img1.GetPixel(j, i);
						int r1 = pix1.R;
						int g1 = pix1.G;
						int b1 = pix1.B;

						//читаем пиксель второй картинки
						var pix2 = img2.GetPixel(j, i);
						int r2 = pix2.R;
						int g2 = pix2.G;
						int b2 = pix2.B;

						//первое задание
						int r = r1 + r2;
						if (r > 255) { r = 255; }
						int g = g1 + g2;
						if (g > 255) { g = 255; }
						int b = b1 + b2;
						if (b > 255) { b = 255; }

						// записываем пиксель в изображение
						var pix = Color.FromArgb(r, g, b);
						img_out.SetPixel(j, i, pix);
					}
				}
				stopwatch1.Stop();
				Console.WriteLine("Изображение обработанно за  " + stopwatch1.ElapsedMilliseconds + " ms ");
				//сохраним нашу выходную картинку 
				Console.WriteLine("Введите имя файла в который вы хотите сохранить полученный результат: ");
				string nameFaleResult = Console.ReadLine();
				img_out.Save("..\\..\\" + nameFaleResult);
				Console.WriteLine("Выходное изображение было сохренено по пути " + Directory.GetParent("..\\..\\") + "\\" + nameFaleResult);
			}
		}
		public static void pixelProductOfTwoImages(int widthImage1, int heightImage1, Bitmap img1, Bitmap img2)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			using (var img_out = new Bitmap(widthImage1, heightImage1))
			{

				for (int i = 0; i < img1.Height; i++)
				{
					for (int j = 0; j < img1.Width; j++)
					{
						//читаем пиксель первой картинки
						var pix1 = img1.GetPixel(j, i);
						int r1 = pix1.R;
						int g1 = pix1.G;
						int b1 = pix1.B;

						//читаем пиксель второй картинки
						var pix2 = img2.GetPixel(j, i);
						int r2 = pix2.R;
						int g2 = pix2.G;
						int b2 = pix2.B;

						//второе задание
						int r = r1 * r2;
						if (r > 255) { r = 255; }
						int g = g1 * g2;
						if (g > 255) { g = 255; }
						int b = b1 * b2;
						if (b > 255) { b = 255; }

						// записываем пиксель в изображение
						var pix = Color.FromArgb(r, g, b);
						img_out.SetPixel(j, i, pix);
					}
				}
				stopwatch.Stop();
				Console.WriteLine("Изображение обработанно за  " + stopwatch.ElapsedMilliseconds + " ms ");
				//сохраним нашу выходную картинку 
				Console.WriteLine("Введите имя файла в который вы хотите сохранить полученный результат: ");
				string nameFaleResult = Console.ReadLine();
				img_out.Save("..\\..\\" + nameFaleResult);
				Console.WriteLine("Выходное изображение было сохренено по пути " + Directory.GetParent("..\\..\\") + "\\" + nameFaleResult);
			}
		}
		public static void pixelArithmeticMeanOfTwoImages(int widthImage1, int heightImage1, Bitmap img1, Bitmap img2)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			using (var img_out = new Bitmap(widthImage1, heightImage1))
			{

				for (int i = 0; i < img1.Height; i++)
				{
					for (int j = 0; j < img1.Width; j++)
					{
						//читаем пиксель первой картинки
						var pix1 = img1.GetPixel(j, i);
						int r1 = pix1.R;
						int g1 = pix1.G;
						int b1 = pix1.B;

						//читаем пиксель второй картинки
						var pix2 = img2.GetPixel(j, i);
						int r2 = pix2.R;
						int g2 = pix2.G;
						int b2 = pix2.B;

						//второе задание
						int r = r1 + r2;
						int g = g1 + g2;
						int b = b1 + b2;

						r = (int)Clamp(r * 0.5, 0, 255);
						g = (int)Clamp(g * 0.5, 0, 255);
						b = (int)Clamp(b * 0.5, 0, 255);
						// записываем пиксель в изображение
						var pix = Color.FromArgb(r, g, b);
						img_out.SetPixel(j, i, pix);
					}
				}
				stopwatch.Stop();
				Console.WriteLine("Изображение обработанно за  " + stopwatch.ElapsedMilliseconds + " ms ");
				//сохраним нашу выходную картинку 
				Console.WriteLine("Введите имя файла в который вы хотите сохранить полученный результат: ");
				string nameFaleResult = Console.ReadLine();
				img_out.Save("..\\..\\" + nameFaleResult);
				Console.WriteLine("Выходное изображение было сохренено по пути " + Directory.GetParent("..\\..\\") + "\\" + nameFaleResult);
			}
		}
		public static void pixelMinOfTwoImages(int widthImage1, int heightImage1, Bitmap img1, Bitmap img2)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			using (var img_out = new Bitmap(widthImage1, heightImage1))
			{

				for (int i = 0; i < img1.Height; i++)
				{
					for (int j = 0; j < img1.Width; j++)
					{
						//читаем пиксель первой картинки
						var pix1 = img1.GetPixel(j, i);
						int r1 = pix1.R;
						int g1 = pix1.G;
						int b1 = pix1.B;

						//читаем пиксель второй картинки
						var pix2 = img2.GetPixel(j, i);
						int r2 = pix2.R;
						int g2 = pix2.G;
						int b2 = pix2.B;

						//минимум
						int r, g, b;
						if (r1 > r2)
						{
							r = r2;
						}
						else { r = r1; }
						if (g1 > g2)
						{
							g = g2;

						} else { g = g1; }
						if (b1 > b2)
						{
							b = b2;
						}
						else { b = b1; }

						// записываем пиксель в изображение
						var pix = Color.FromArgb(r, g, b);
						img_out.SetPixel(j, i, pix);
					}
				}
				stopwatch.Stop();
				Console.WriteLine("Изображение обработанно за  " + stopwatch.ElapsedMilliseconds + " ms ");
				//сохраним нашу выходную картинку 
				Console.WriteLine("Введите имя файла в который вы хотите сохранить полученный результат: ");
				string nameFaleResult = Console.ReadLine();
				img_out.Save("..\\..\\" + nameFaleResult);
				Console.WriteLine("Выходное изображение было сохренено по пути " + Directory.GetParent("..\\..\\") + "\\" + nameFaleResult);
			}
		}
		public static void pixelMaxOfTwoImages(int widthImage1, int heightImage1, Bitmap img1, Bitmap img2)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			using (var img_out = new Bitmap(widthImage1, heightImage1))
			{

				for (int i = 0; i < img1.Height; i++)
				{
					for (int j = 0; j < img1.Width; j++)
					{
						//читаем пиксель первой картинки
						var pix1 = img1.GetPixel(j, i);
						int r1 = pix1.R;
						int g1 = pix1.G;
						int b1 = pix1.B;

						//читаем пиксель второй картинки
						var pix2 = img2.GetPixel(j, i);
						int r2 = pix2.R;
						int g2 = pix2.G;
						int b2 = pix2.B;

						//максимум
						int r, g, b;
						if (r1 > r2)
						{
							r = r1;
						}
						else { r = r2; }
						if (g1 > g2)
						{
							g = g1;

						}
						else { g = g2; }
						if (b1 > b2)
						{
							b = b1;
						}
						else { b = b2; }

						// записываем пиксель в изображение
						var pix = Color.FromArgb(r, g, b);
						img_out.SetPixel(j, i, pix);
					}
				}
				stopwatch.Stop();
				Console.WriteLine("Изображение обработанно за  " + stopwatch.ElapsedMilliseconds + " ms ");
				//сохраним нашу выходную картинку 
				Console.WriteLine("Введите имя файла в который вы хотите сохранить полученный результат: ");
				string nameFaleResult = Console.ReadLine();
				img_out.Save("..\\..\\" + nameFaleResult);
				Console.WriteLine("Выходное изображение было сохренено по пути " + Directory.GetParent("..\\..\\") + "\\" + nameFaleResult);
			}
		}
		public static void applyMaskToImage(int widthImage1, int heightImage1, Bitmap img1, Bitmap img2)
		{
			Console.WriteLine("Выбирите какой формы вы хотите наложить маску на изображение: ");
			Console.WriteLine("Нажмите '1' если хотите маску круг");
			Console.WriteLine("Нажмите '2' если хотите маску квадрат");
			Console.WriteLine("Нажмите '3' если хотите маску прямоугольник");
			int caseSwitch = Convert.ToInt32(Console.ReadLine());
			switch (caseSwitch)
			{
				case 1:
					maskCircle(widthImage1, heightImage1, img1, img2);
					break;
				case 2:
					maskSquare(widthImage1, heightImage1, img1, img2);
					break;
				case 3:
					maskRectange(widthImage1, heightImage1, img1, img2);
					break;
			}
			
		}
		public static void maskCircle(int widthImage1, int heightImage1, Bitmap img1, Bitmap img2) // Circle ready 
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			using (var img_out = new Bitmap(widthImage1, heightImage1))
			{
				int center_x = img2.Width / 2; // центр по ширине
				int center_y = img2.Height / 2; // центр по высоте
				int radius = img2.Width / 8;
				
				// пытаюсь
				double fi = 360;

				for (int i = 0; i < img1.Height; i++)
				{
					for (int j = 0; j < img1.Width; j++)
					{
						// в начале находим среднее ширину потом высоту и находим средний пиксель
						// затем делаем квадратное окно относительно масшатабов картинки
						var pix1 = img1.GetPixel(j, i);
						int r = pix1.R;
						int g = pix1.G;
						int b = pix1.B;
						//находим точку на окружности
						double res = Math.Pow(j - center_x, 2) + Math.Pow(i - center_y, 2);

						if (res <= Math.Pow(radius, 2))
						{
							var pix2 = img2.GetPixel(j, i);
							r = pix2.R;
							g = pix2.G;
							b = pix2.B;
						}
						// записываем пиксель в изображение
						var pix = Color.FromArgb(r, g, b);
						img_out.SetPixel(j, i, pix);
					}
				}

				stopwatch.Stop();
				Console.WriteLine("Изображение обработанно за  " + stopwatch.ElapsedMilliseconds + " ms ");
				//сохраним нашу выходную картинку 
				Console.WriteLine("Введите имя файла в который вы хотите сохранить полученный результат: ");
				string nameFaleResult = Console.ReadLine();
				img_out.Save("..\\..\\" + nameFaleResult);
				Console.WriteLine("Выходное изображение было сохренено по пути " + Directory.GetParent("..\\..\\") + "\\" + nameFaleResult);
			}
		}
		public static void maskSquare(int widthImage1, int heightImage1, Bitmap img1, Bitmap img2) // Square ready
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			using (var img_out = new Bitmap(widthImage1, heightImage1))
			{
				int center_x = img2.Width / 2;
				int center_y = img2.Height / 2;
				int расстояние_отступа = (img2.Height / 5);

				for (int i = 0; i < img1.Height; i++)
				{
					for (int j = 0; j < img1.Width; j++)
					{
						// в начале находим среднее ширину потом высоту и находим средний пиксель
						// затем делаем квадратное окно относительно масшатабов картинки
						var pix1 = img1.GetPixel(j, i);
						int r = pix1.R;
						int g = pix1.G;
						int b = pix1.B;
						
						if ((j < center_x - расстояние_отступа) || (j > center_x + расстояние_отступа) || (i > center_y + расстояние_отступа) || (i < center_y - расстояние_отступа)) // печатать вторую фотку все кроме ограничений
						{
							var pix2 = img2.GetPixel(j, i);
							r = pix2.R;
							g = pix2.G;
							b = pix2.B;
						}
						// записываем пиксель в изображение
						var pix = Color.FromArgb(r, g, b);
						img_out.SetPixel(j, i, pix);
					}
				}
				
				stopwatch.Stop();
				Console.WriteLine("Изображение обработанно за  " + stopwatch.ElapsedMilliseconds + " ms ");
				//сохраним нашу выходную картинку 
				Console.WriteLine("Введите имя файла в который вы хотите сохранить полученный результат: ");
				string nameFaleResult = Console.ReadLine();
				img_out.Save("..\\..\\" + nameFaleResult);
				Console.WriteLine("Выходное изображение было сохренено по пути " + Directory.GetParent("..\\..\\") + "\\" + nameFaleResult);
			}
		}
		public static void maskRectange(int widthImage1, int heightImage1, Bitmap img1, Bitmap img2) // rectangle ready
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			using (var img_out = new Bitmap(widthImage1, heightImage1))
			{
				int center_x = img2.Width / 2;
				int center_y = img2.Height / 2;
				int вверхняя_граница_прямоугольника = center_y + (img2.Height / 4);
				int нижняя_граница_прямоугольника = center_y - (img2.Height / 4);
				int лево = center_x - (img2.Width / 4);
				int прав = center_x + (img2.Width / 4);

				for (int i = 0; i < img1.Height; i++)
				{
					for (int j = 0; j < img1.Width; j++)
					{
						// в начале находим среднее ширину потом высоту и находим средний пиксель
						// затем делаем прямоугольное окно относительно масшатабов картинки
						var pix1 = img1.GetPixel(j, i);
						int r = pix1.R;
						int g = pix1.G;
						int b = pix1.B;
						
						if ((j < лево) || (j>прав) || (i>вверхняя_граница_прямоугольника) || (i<нижняя_граница_прямоугольника)) // печатать вторую фотку все кроме ограничений
						{
							var pix2 = img2.GetPixel(j, i);
							r = pix2.R;
							g = pix2.G;
							b = pix2.B;
						}
						// записываем пиксель в изображение
						var pix = Color.FromArgb(r, g, b);
						img_out.SetPixel(j, i, pix);
					}
				}
				stopwatch.Stop();
				Console.WriteLine("Изображение обработанно за  " + stopwatch.ElapsedMilliseconds + " ms ");
				Console.WriteLine("Введите имя файла в который вы хотите сохранить полученный результат: ");
				string nameFaleResult = Console.ReadLine();
				img_out.Save("..\\..\\" + nameFaleResult);
				Console.WriteLine("Выходное изображение было сохренено по пути " + Directory.GetParent("..\\..\\") + "\\" + nameFaleResult);
			}
		} 
		public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
		{
			if (val.CompareTo(min) < 0) return min;
			else if (val.CompareTo(max) > 0) return max;
			else return val;
		}

		#endregion
	}
}
