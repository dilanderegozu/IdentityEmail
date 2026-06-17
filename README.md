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

🔑 Kullanıcı Giriş Ekranı (Login Page)
✅ Güvenli Kimlik Doğrulama: ASP.NET Identity altyapısı kullanılarak kullanıcıların güvenli şekilde sisteme giriş yapması sağlanmıştır.
✅ Oturum Yönetimi: Başarılı giriş sonrasında kullanıcı bilgileri kimlik doğrulama mekanizması ile yönetilerek güvenli oturum oluşturulur.
✅ Beni Hatırla Özelliği: Kalıcı çerez (Persistent Cookie) desteği sayesinde kullanıcılar tarayıcıyı kapatsalar bile oturumlarını koruyabilirler.
✅ Gerçek Zamanlı Hata Bildirimleri: Hatalı kullanıcı adı veya şifre girişlerinde ModelState kullanılarak kullanıcıya anlık geri bildirim sağlanır.
✅ Yetkilendirme Altyapısı: Kimliği doğrulanmış kullanıcıların sistem içerisindeki yetkili sayfalara erişimi kontrol edilir.
✅ Kullanıcı Deneyimi Odaklı Tasarım: Modern ve sade giriş ekranı sayesinde hızlı ve kolay oturum açma deneyimi sunulur.
<img width="1869" height="953" alt="3" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/giris" />
<img width="1869" height="953" alt="3" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/giris2" />

📧 1. Şifre Sıfırlama Talebi (Forgot Password)
✅ E-Posta Doğrulama Süreci: Kullanıcının sisteme kayıtlı e-posta adresi kontrol edilerek şifre sıfırlama işlemi başlatılır.
✅ Güvenli Talep Oluşturma: Kimliği doğrulanan kullanıcı için özel bir şifre sıfırlama bağlantısı üretilir.
✅ Anlık Geri Bildirim: Talebin başarıyla oluşturulduğu kullanıcıya bilgilendirme mesajları ile iletilir.
✅ Identity Entegrasyonu: ASP.NET Identity altyapısı kullanılarak güvenli parola kurtarma mekanizması sağlanmıştır.
✅ Kullanıcı Dostu Arayüz: Sade ve anlaşılır ekran tasarımı sayesinde şifre sıfırlama işlemi kolaylaştırılmıştır.
<img width="525" height="653" alt="4" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/forgotpassword" />
📨 2. Şifre Sıfırlama E-Postası (Reset Password Mail)
✅ HTML Tabanlı E-Posta Şablonu: MimeKit kullanılarak kurumsal tasarıma sahip, kullanıcı dostu e-posta içeriği hazırlanmıştır.
✅ Benzersiz Güvenlik Tokenı: Her kullanıcı için özel olarak üretilen şifre sıfırlama tokenı bağlantı içerisine eklenir.
✅ Tek Kullanımlık Sıfırlama Linki: Kullanıcı yalnızca kendisine gönderilen bağlantı üzerinden parola yenileme işlemi gerçekleştirebilir.
✅ SMTP Mail Servisi: MailKit altyapısı ile e-postalar güvenilir şekilde kullanıcıya ulaştırılır.
✅ Güvenli Hesap Kurtarma: Yetkisiz erişimleri önlemek amacıyla bağlantılar kullanıcıya özel olarak oluşturulur ve doğrulanır.
<img width="1618" height="358" alt="6" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/linkresetpassword" />
🔒 3. Yeni Şifre Oluşturma (Reset Password)
✅ Token Doğrulama: E-posta üzerinden gelen bağlantıdaki token doğrulanarak kullanıcının parola değiştirme yetkisi kontrol edilir.
✅ Güvenli Parola Güncelleme: Yeni parola ASP.NET Identity altyapısı üzerinden güvenli şekilde güncellenir.
✅ Şifre Politikaları: Minimum uzunluk ve karmaşıklık kuralları uygulanarak hesap güvenliği artırılır.
✅ Başarılı İşlem Bildirimi: Parola güncelleme işlemi tamamlandığında kullanıcıya bilgilendirme mesajı gösterilir.
✅ Yetkisiz Erişim Koruması: Geçersiz veya süresi dolmuş bağlantılar sistem tarafından engellenir.
<img width="1618" height="358" alt="6" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20021853.png" />
