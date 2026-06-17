# 🚀 YUDI Admin Panelli | Kullanıcı Kimlik ve Mesajlaşma Yönetim Sistemi
Bu proje, ASP.NET Core 10.0 kullanılarak geliştirilmiş, gelişmiş kullanıcı yönetim sistemine (Identity) ve gerçek zamanlı e-posta doğrulama mekanizmasına sahip modern bir yönetim panelidir. 
Kullanıcı deneyimini ön planda tutan AJAX tabanlı doğrulamalar ve kurumsal seviyede bir mesajlaşma altyapısı sunar.


 🔐 KULLANICI KAYIT VE GÜVENLİK SÜRECİ (REGISTER & 2FA)
📸 1. Kullanıcı Kayıt Ekranı (Register Page)
✅ Modern ve Kullanıcı Dostu Tasarım: Admin arayüzü ile uyumlu, responsive yapıda geliştirilmiş kayıt ekranı.
✅ Profil Fotoğrafı Yükleme: Kullanıcılar kayıt sırasında profil fotoğrafı ekleyebilir. Yüklenen dosyalar IFormFile kullanılarak sunucuya aktarılır ve wwwroot/UserImages/ dizininde güvenli şekilde saklanır.
✅ Kimlik Doğrulama Entegrasyonu: Kayıt işlemi tamamlandığında sistem tarafından otomatik olarak benzersiz 6 haneli doğrulama kodu oluşturulur.
✅ Asenkron İşlem Yönetimi: Dosya yükleme ve kullanıcı oluşturma süreçleri performans odaklı olarak asenkron şekilde gerçekleştirilir.
✅ Güvenli Hesap Oluşturma: Kullanıcı bilgileri ASP.NET Identity altyapısı kullanılarak veritabanına kaydedilir ve doğrulama süreci başlatılır.
<img width="1864" height="953" alt="1" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/register" />

📧 2. E-Posta Doğrulama Kodu Gönderimi
✅ SMTP Tabanlı E-Posta Servisi: MailKit ve MimeKit kütüphaneleri kullanılarak Gmail SMTP üzerinden güvenilir ve hızlı e-posta gönderimi sağlanmıştır.
✅ Otomatik Doğrulama Kodu Üretimi: Kayıt işlemi sonrasında kullanıcıya özel rastgele 6 haneli bir doğrulama kodu oluşturulur.
✅ Hesap Güvenliği: Oluşturulan doğrulama kodu kullanıcının e-posta adresine iletilerek hesap sahipliği doğrulanır.
✅ İki Aşamalı Doğrulama Altyapısı: Gönderilen kod sayesinde kullanıcı hesabının güvenli şekilde aktifleştirilmesi sağlanır.
✅ Dinamik E-Posta İçeriği: Kullanıcıya özel hazırlanan HTML tabanlı e-posta şablonları ile doğrulama süreci desteklenir.
<img width="692" height="886" alt="2" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/confirmemail" />

🔐 3. AJAX Tabanlı E-Posta Doğrulama Modalı
✅ Kesintisiz Kullanıcı Deneyimi: Form gönderimleri AJAX ile gerçekleştirilerek sayfa yenilemeden doğrulama işlemi sağlanmıştır.
✅ Dinamik Doğrulama Penceresi: Kayıt işlemi sonrasında doğrulama modalı otomatik olarak açılarak kullanıcı yönlendirilir.
✅ Yüklenme Durumu Yönetimi: İşlem süresince kullanıcıya "Lütfen Bekleyin..." mesajı, spinner animasyonu ve buton durum değişiklikleri gösterilir.
✅ Anlık Kod Kontrolü: Girilen doğrulama kodu sunucu tarafında doğrulanır ve sonuç anlık olarak kullanıcıya iletilir.
✅ Otomatik Yönlendirme: Başarılı doğrulama sonrasında kullanıcı giriş ekranına veya ilgili sayfaya otomatik olarak yönlendirilir.
✅ Asenkron Veri İşleme: Sayfa yenilenmeden gerçekleştirilen işlemler sayesinde daha hızlı ve modern bir kullanıcı deneyimi sunulur.
<img width="1864" height="953" alt="3" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/confirmemaildetail"/>
