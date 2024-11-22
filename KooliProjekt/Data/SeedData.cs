using KooliProjekt.Data;
using KooliProjekt.Models;
using System;
using System.Linq;

public static class SeedData
{
    public static void Generate(ApplicationDbContext dbContext)
    {
        // Kontrollige, et HealthData on tühi, enne kui andmeid lisate
        if (!dbContext.health_data.Any())
        {
            dbContext.health_data.AddRange(
                new HealthData
                {
                    UserId = 1,
                    User = "John Doe",
                    Date = "2024-11-01",
                    BloodSugar = 70,
                    Weight = 60,
                    BloodAir = "Normal",
                    Systolic = 120,
                    Diastolic = 80,
                    Pulse = "72",
                    Height = 180,
                    Age = 25
                },
                new HealthData
                {
                    UserId = 2,
                    User = "Jane Smith",
                    Date = "2024-11-02",
                    BloodSugar = 72,
                    Weight = 65,
                    BloodAir = "Normal",
                    Systolic = 115,
                    Diastolic = 75,
                    Pulse = "75",
                    Height = 165,
                    Age = 30
                },
                new HealthData
                {
                    UserId = 3,
                    User = "Alice Johnson",
                    Date = "2024-11-03",
                    BloodSugar = 74,
                    Weight = 70,
                    BloodAir = "Normal",
                    Systolic = 125,
                    Diastolic = 80,
                    Pulse = "78",
                    Height = 170,
                    Age = 28
                },
                new HealthData
                {
                    UserId = 4,
                    User = "Bob Brown",
                    Date = "2024-11-04",
                    BloodSugar = 76,
                    Weight = 75,
                    BloodAir = "Normal",
                    Systolic = 130,
                    Diastolic = 85,
                    Pulse = "80",
                    Height = 175,
                    Age = 35
                },
                new HealthData
                {
                    UserId = 5,
                    User = "Charlie Davis",
                    Date = "2024-11-05",
                    BloodSugar = 78,
                    Weight = 80,
                    BloodAir = "Normal",
                    Systolic = 135,
                    Diastolic = 90,
                    Pulse = "82",
                    Height = 180,
                    Age = 40
                },
                new HealthData
                {
                    UserId = 6,
                    User = "David Wilson",
                    Date = "2024-11-06",
                    BloodSugar = 80,
                    Weight = 85,
                    BloodAir = "Normal",
                    Systolic = 140,
                    Diastolic = 95,
                    Pulse = "85",
                    Height = 185,
                    Age = 45
                },
                new HealthData
                {
                    UserId = 7,
                    User = "Emma White",
                    Date = "2024-11-07",
                    BloodSugar = 82,
                    Weight = 90,
                    BloodAir = "Normal",
                    Systolic = 145,
                    Diastolic = 100,
                    Pulse = "88",
                    Height = 160,
                    Age = 50
                },
                new HealthData
                {
                    UserId = 8,
                    User = "Frank Harris",
                    Date = "2024-11-08",
                    BloodSugar = 84,
                    Weight = 95,
                    BloodAir = "Normal",
                    Systolic = 150,
                    Diastolic = 105,
                    Pulse = "90",
                    Height = 170,
                    Age = 55
                },
                new HealthData
                {
                    UserId = 9,
                    User = "Grace Martinez",
                    Date = "2024-11-09",
                    BloodSugar = 86,
                    Weight = 100,
                    BloodAir = "Normal",
                    Systolic = 155,
                    Diastolic = 110,
                    Pulse = "92",
                    Height = 165,
                    Age = 60
                },
                new HealthData
                {
                    UserId = 10,
                    User = "Hannah Lee",
                    Date = "2024-11-10",
                    BloodSugar = 88,
                    Weight = 105,
                    BloodAir = "Normal",
                    Systolic = 160,
                    Diastolic = 115,
                    Pulse = "95",
                    Height = 160,
                    Age = 65
                }
            );

            dbContext.SaveChanges();  // Salvestame muudatused andmebaasi
        }
        if (!dbContext.nutrients.Any())  // Make sure this is "Nutrients" and not "nutrients"
        {
            dbContext.nutrients.AddRange(
                new Nutrients { Name = "Carbohydrates", Sugars = 0, Fats = 0, Carbohydrates = 30 },
                new Nutrients { Name = "Proteins", Sugars = 0, Fats = 0, Carbohydrates = 25 },
                new Nutrients { Name = "Fats", Sugars = 0, Fats = 15, Carbohydrates = 0 },
                new Nutrients { Name = "Fiber", Sugars = 0, Fats = 0, Carbohydrates = 5 },
                new Nutrients { Name = "Vitamin C", Sugars = 15, Fats = 0, Carbohydrates = 0 },
                new Nutrients { Name = "Calcium", Sugars = 0, Fats = 0, Carbohydrates = 10 },
                new Nutrients { Name = "Iron", Sugars = 0, Fats = 0, Carbohydrates = 5 },
                new Nutrients { Name = "Potassium", Sugars = 0, Fats = 0, Carbohydrates = 20 },
                new Nutrients { Name = "Magnesium", Sugars = 0, Fats = 0, Carbohydrates = 7 },
                new Nutrients { Name = "Sodium", Sugars = 0, Fats = 0, Carbohydrates = 2 }
            );

            // Kontrollige, kas FoodChart tabel on tühi
            if (!dbContext.food_Chart.Any())
            {
                dbContext.food_Chart.AddRange(
                    new FoodChart { InvoiceNo = "FC001", InvoiceDate = DateTime.Now, user = "Madu", date = "2024-11-22", meal = "Breakfast", nutrients = DateTime.Now, amount = 30 },
                    new FoodChart { InvoiceNo = "FC002", InvoiceDate = DateTime.Now, user = "Siim", date = "2024-11-22", meal = "Lunch", nutrients = DateTime.Now, amount = 50 },
                    new FoodChart { InvoiceNo = "FC003", InvoiceDate = DateTime.Now, user = "Kati", date = "2024-11-22", meal = "Dinner", nutrients = DateTime.Now, amount = 40 },
                    new FoodChart { InvoiceNo = "FC004", InvoiceDate = DateTime.Now, user = "Mati", date = "2024-11-22", meal = "Snack", nutrients = DateTime.Now, amount = 25 },
                    new FoodChart { InvoiceNo = "FC005", InvoiceDate = DateTime.Now, user = "Mari", date = "2024-11-22", meal = "Breakfast", nutrients = DateTime.Now, amount = 35 },
                    new FoodChart { InvoiceNo = "FC006", InvoiceDate = DateTime.Now, user = "Tõnu", date = "2024-11-22", meal = "Lunch", nutrients = DateTime.Now, amount = 45 },
                    new FoodChart { InvoiceNo = "FC007", InvoiceDate = DateTime.Now, user = "Liis", date = "2024-11-22", meal = "Dinner", nutrients = DateTime.Now, amount = 55 },
                    new FoodChart { InvoiceNo = "FC008", InvoiceDate = DateTime.Now, user = "Jüri", date = "2024-11-22", meal = "Snack", nutrients = DateTime.Now, amount = 30 },
                    new FoodChart { InvoiceNo = "FC009", InvoiceDate = DateTime.Now, user = "Olev", date = "2024-11-22", meal = "Breakfast", nutrients = DateTime.Now, amount = 50 },
                    new FoodChart { InvoiceNo = "FC010", InvoiceDate = DateTime.Now, user = "Ene", date = "2024-11-22", meal = "Lunch", nutrients = DateTime.Now, amount = 60 }
                );



                if (!dbContext.amount.Any())
                {
                    dbContext.amount.AddRange(
                        new Amount { NutrientsID = 1, AmountDate = DateTime.Now },
                        new Amount { NutrientsID = 2, AmountDate = DateTime.Now },
                        new Amount { NutrientsID = 3, AmountDate = DateTime.Now },
                        new Amount { NutrientsID = 4, AmountDate = DateTime.Now },
                        new Amount { NutrientsID = 5, AmountDate = DateTime.Now },
                        new Amount { NutrientsID = 6, AmountDate = DateTime.Now },
                        new Amount { NutrientsID = 7, AmountDate = DateTime.Now },
                        new Amount { NutrientsID = 8, AmountDate = DateTime.Now },
                        new Amount { NutrientsID = 9, AmountDate = DateTime.Now },
                        new Amount { NutrientsID = 10, AmountDate = DateTime.Now }
                    );

                    dbContext.SaveChanges();
                }
            }
        }
    }
}