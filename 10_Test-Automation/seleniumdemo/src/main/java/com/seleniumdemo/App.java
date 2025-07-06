package com.seleniumdemo;


 
import java.lang.reflect.Type;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;


public class App 
{
    public static void main( String[] args )
    {
        WebDriver driver = new ChromeDriver();
        
        try {
            // Giriş sayfasına git
            driver.get("https://proteinocean.com/account/login");
            driver.manage().window().maximize();

            // Kullanıcı adı gir
            WebElement email = driver.findElement(By.cssSelector("input[type='email']"));
            email.sendKeys("egaxbora@gmail.com");

            // Şifre gir
            WebElement password = driver.findElement(By.cssSelector("input[type='password']"));
            password.sendKeys("");

            // Login butonuna tıkla
            WebElement loginButton = driver.findElement(By.cssSelector("button[type='submit']"));
            loginButton.click();

            // Sayfanın yönlendirilip yönlendirilmediğini kontrol et
            Thread.sleep(2000);
            if (driver.getCurrentUrl().contains("account")) {
                System.out.println("Başarıyla giriş yapıldı.");
            } else {
                System.out.println("Giriş başarısız.");
            }
            Thread.sleep(20000);

        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            // Tarayıcıyı kapat
            driver.quit();
        }
   }
}
