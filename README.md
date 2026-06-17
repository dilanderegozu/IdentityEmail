# 🚀 YUDI Admin Panelli | Kullanıcı Kimlik ve Mesajlaşma Yönetim Sistemi
Bu proje, ASP.NET Core 10.0 kullanılarak geliştirilmiş, gelişmiş kullanıcı yönetim sistemine (Identity) ve gerçek zamanlı e-posta doğrulama mekanizmasına sahip modern bir yönetim panelidir. 
Kullanıcı deneyimini ön planda tutan AJAX tabanlı doğrulamalar ve kurumsal seviyede bir mesajlaşma altyapısı sunar.


## 🔐 KULLANICI KAYIT VE GÜVENLİK SÜRECİ (REGISTER & 2FA)
📸 1. Kullanıcı Kayıt Ekranı (Register Page)
✅ Modern ve Kullanıcı Dostu Tasarım: Mendy Admin arayüzü ile uyumlu, responsive yapıda geliştirilmiş kayıt ekranı.
✅ Profil Fotoğrafı Yükleme: Kullanıcılar kayıt sırasında profil fotoğrafı ekleyebilir. Yüklenen dosyalar IFormFile kullanılarak sunucuya aktarılır ve wwwroot/UserImages/ dizininde güvenli şekilde saklanır.
✅ Kimlik Doğrulama Entegrasyonu: Kayıt işlemi tamamlandığında sistem tarafından otomatik olarak benzersiz 6 haneli doğrulama kodu oluşturulur.
✅ Asenkron İşlem Yönetimi: Dosya yükleme ve kullanıcı oluşturma süreçleri performans odaklı olarak asenkron şekilde gerçekleştirilir.
✅ Güvenli Hesap Oluşturma: Kullanıcı bilgileri ASP.NET Identity altyapısı kullanılarak veritabanına kaydedilir ve doğrulama süreci başlatılır.
<img width="1864" height="953" alt="1" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/register" />


