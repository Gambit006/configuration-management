# Configuration Management Documentation


Configuration Management kütüphanesi eklendiği projenin konfigürasyonunu tek bir yerden merkezi olarak yönetilmesini için geliştirlimiştir. 

## Kullanılan teknolojiler:
- .net Core 8
- mongodb
- xUnit
- .net core 8 MVC

## Projesin kapsamı:
**ConfigurationManagement Class Library Project:** İstenilen projede kullanılmak için gerekli konfigürasyonların getirilmesi işlemleri bu kütüphane yapar. 

**ConfigurationUnitTest Projesi:**
Bu proje ConfigurationManagement çıktıları için unit test yapılmıştır. 

**ConfigurationRecordsUtilities: **İlgili konfigürasyonların storagedan getirilmesi işlemini yapar.
ConfigurationReader:  Konfigürasyonları alır ve belirli zaman aralıklarında kontrol eder. 

### TestApp Projesi:
 ConfigurationManagement kütüphanesinin kullanıcı bazlı testi yapılmıştır.

### WebApp Projesi: 
Kullanıcıların ilgili tüm konfigürasyonları görmesi ve düzenlemesi için MVC ile geliştirilmiş bir paneldir. 

## Proje Planlama
Projede waterfall yazılım geliştirme planı uygulanmıştır. Ve sırasıyla aşağıdaki geliştirme süreçleri takip edilmiştir. 
1. ConfigurationRecord Model tasarımı
2. Veritabanın kurulması ve ilgili sınıfın yazılamsı
3. UnitTestlerin gerçekleştirilmesi
4. end-to-end testlerin consol app’te gerçekleştirilmesi.
5. UI olarak MVC projesinin geliştirilmesi ve test edilmesi









