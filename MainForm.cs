using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project;

public class MainForm : Form
{
    private System.Windows.Forms.Timer timer;

    public MainForm()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        // Налаштування вікна
        Size = new Size(600, 600);
        Text = "Годинник";

        // Створення таймера
        timer = new System.Windows.Forms.Timer();
        timer.Interval = 1000; // Оновлення кожну секунду
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        Invalidate(); // Запускає оновлення вікна
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        // Отримання поточного часу
        DateTime currentTime = DateTime.Now;

        // Отримання значень годин, хвилин та секунд
        int hours = currentTime.Hour % 12; // 12-годинний формат
        int minutes = currentTime.Minute;
        int seconds = currentTime.Second;

        // Розрахунок кутів для стрілок
        float hourAngle = (hours * 30) + (minutes * 0.5f);
        float minuteAngle = (minutes * 6) + (seconds * 0.1f);
        float secondAngle = seconds * 6;

        // Отримання центру вікна
        PointF center = new PointF(285, 260);

        // Відображення годинникової стрілки
        using (Pen hourPen = new Pen(Color.Red, 4))
        {
            PointF hourEnd = CalculatePointOnCircle(center, 80, hourAngle);
            g.DrawLine(hourPen, center, hourEnd);
        }

        // Відображення хвилинної стрілки
        using (Pen minutePen = new Pen(Color.Blue, 3))
        {
            PointF minuteEnd = CalculatePointOnCircle(center, 120, minuteAngle);
            g.DrawLine(minutePen, center, minuteEnd);
        }

        // Відображення секундної стрілки
        using (Pen secondPen = new Pen(Color.Green, 2))
        {
            PointF secondEnd = CalculatePointOnCircle(center, 140, secondAngle);
            g.DrawLine(secondPen, center, secondEnd);
        }

        // Відображення цифр часу
        using (Font font = new Font(FontFamily.GenericSansSerif, 12))
{
    for (int i = 0; i < 12; i++)
    {
        float angle = i * 30 - 60;
        PointF numberPosition = CalculatePointOnCircle(center, 160, angle);
        g.DrawString((i + 1).ToString(), font, Brushes.Black, numberPosition);
    }
}
    }

    private PointF CalculatePointOnCircle(PointF center, float radius, float angle)
    {
        float x = center.X + radius * (float)Math.Cos(angle * Math.PI / 180);
        float y = center.Y + radius * (float)Math.Sin(angle * Math.PI / 180);
        return new PointF(x, y);
    }
}
