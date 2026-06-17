# 🚀 YUDI Admin Paneli | Kullanıcı Kimlik ve Mesajlaşma Yönetim Sistemi

Bu proje, **ASP.NET Core** ve **ASP.NET Identity** teknolojileri kullanılarak geliştirilmiş modern bir kullanıcı yönetim sistemidir. Sistem içerisinde kullanıcı kayıt işlemleri, e-posta doğrulama mekanizması, güvenli giriş işlemleri, parola kurtarma süreçleri ve AJAX tabanlı kullanıcı deneyimini geliştiren birçok özellik bulunmaktadır.

Kullanıcı güvenliğini ön planda tutan proje; iki aşamalı doğrulama (2FA), SMTP entegrasyonu, token tabanlı parola yenileme sistemi ve modern yönetim paneli tasarımı ile kurumsal uygulamalarda kullanılabilecek güçlü bir altyapı sunmaktadır.

---

# 🔐 Kullanıcı Kayıt ve Güvenlik Süreci (Register & 2FA)

## 📸 1. Kullanıcı Kayıt Ekranı (Register Page)

Modern ve kullanıcı dostu kayıt ekranı sayesinde kullanıcılar sisteme hızlı ve güvenli şekilde kayıt olabilmektedir.

### Özellikler

* ✅ Responsive ve modern tasarım
* ✅ Profil fotoğrafı yükleme desteği (`IFormFile`)
* ✅ Dosyaların `wwwroot/UserImages/` dizininde güvenli saklanması
* ✅ Otomatik 6 haneli doğrulama kodu üretimi
* ✅ Asenkron kullanıcı oluşturma işlemleri
* ✅ ASP.NET Identity ile güvenli hesap oluşturma

<img width="1864" height="953" alt="Register" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/register" />

---

## 📧 2. E-Posta Doğrulama Kodu Gönderimi

Kayıt işlemi tamamlandıktan sonra kullanıcıya özel oluşturulan doğrulama kodu e-posta adresine gönderilir ve hesap doğrulama süreci başlatılır.

### Özellikler

* ✅ MailKit ve MimeKit ile SMTP entegrasyonu
* ✅ Gmail SMTP üzerinden e-posta gönderimi
* ✅ Kullanıcıya özel 6 haneli doğrulama kodu üretimi
* ✅ Hesap sahipliği doğrulaması
* ✅ İki aşamalı doğrulama (2FA) desteği
* ✅ HTML tabanlı dinamik e-posta şablonu

<img width="692" height="886" alt="Confirm Mail" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/confirmemail" />

---

## 🔐 3. AJAX Tabanlı E-Posta Doğrulama Modalı

Doğrulama işlemleri sayfa yenilenmeden gerçekleştirilerek kullanıcı deneyimi üst seviyeye taşınmıştır.

### Özellikler

* ✅ AJAX ile asenkron doğrulama işlemleri
* ✅ Dinamik doğrulama modalı
* ✅ Loading spinner ve durum yönetimi
* ✅ Anlık doğrulama kontrolü
* ✅ Başarılı işlem sonrası otomatik yönlendirme
* ✅ Kesintisiz kullanıcı deneyimi

<img width="1864" height="953" alt="Verification Modal" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/confirmemaildetail"/>

---

# 🔑 Oturum Yönetimi ve Kimlik Doğrulama Sistemi (Login)

## 📸 Kullanıcı Giriş Ekranı

ASP.NET Identity altyapısı kullanılarak kullanıcıların sisteme güvenli şekilde giriş yapması sağlanmıştır.

### Özellikler

* ✅ Güvenli kimlik doğrulama
* ✅ Oturum yönetimi
* ✅ Beni Hatırla (Persistent Cookie) desteği
* ✅ ModelState ile hata yönetimi
* ✅ Yetkilendirme altyapısı
* ✅ Modern kullanıcı deneyimi

<img width="1869" height="953" alt="Login" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/giris" />

<img width="1869" height="953" alt="Login Error" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/giris2" />

---

# 🛠️ Şifre Kurtarma ve Yenileme Süreci (Password Reset)

## 📧 1. Şifre Sıfırlama Talebi (Forgot Password)

Kullanıcılar kayıtlı e-posta adreslerini kullanarak güvenli şekilde parola sıfırlama talebinde bulunabilirler.

### Özellikler

* ✅ E-posta doğrulama süreci
* ✅ Güvenli sıfırlama talebi oluşturma
* ✅ Anlık kullanıcı bilgilendirmesi
* ✅ ASP.NET Identity entegrasyonu
* ✅ Kullanıcı dostu arayüz

<img width="525" height="653" alt="Forgot Password" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/forgotpassword" />

---

## 📨 2. Şifre Sıfırlama E-Postası (Reset Password Mail)

Sisteme kayıtlı kullanıcılara özel güvenlik tokenı içeren parola yenileme bağlantısı gönderilir.

### Özellikler

* ✅ HTML tabanlı kurumsal e-posta tasarımı
* ✅ MimeKit ile dinamik içerik üretimi
* ✅ Kullanıcıya özel güvenlik tokenı
* ✅ Tek kullanımlık parola sıfırlama bağlantısı
* ✅ SMTP üzerinden güvenli e-posta gönderimi
* ✅ Güvenli hesap kurtarma süreci

<img width="1618" height="358" alt="Reset Password Mail" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/linkresetpassword" />

---

## 🔒 3. Yeni Şifre Oluşturma (Reset Password)

Kullanıcılar kendilerine gönderilen güvenli bağlantı üzerinden yeni parolalarını oluşturabilirler.

### Özellikler

* ✅ Token doğrulama işlemi
* ✅ Güvenli parola güncelleme
* ✅ Şifre karmaşıklık kuralları
* ✅ Başarılı işlem bildirimleri
* ✅ Geçersiz veya süresi dolmuş bağlantılara karşı koruma

<img width="1618" height="358" alt="Reset Password" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20021853.png" />

---

# 🛠️ Kullanılan Teknolojiler

* ASP.NET Core
* ASP.NET Identity
* Entity Framework Core
* SQL Server
* MailKit
* MimeKit
* AJAX
* jQuery
* Bootstrap
* HTML5
* CSS3
* JavaScript

---

# 🎯 Projede Gerçekleştirilen Temel Özellikler

* Kullanıcı Kayıt Sistemi
* E-Posta Doğrulama Mekanizması
* İki Aşamalı Doğrulama (2FA)
* Profil Fotoğrafı Yükleme
* Güvenli Kimlik Doğrulama
* Kalıcı Oturum Yönetimi
* Şifre Kurtarma Sistemi
* Token Tabanlı Şifre Yenileme
* AJAX Tabanlı Form İşlemleri
* Responsive Yönetim Paneli
* SMTP Mail Servisi Entegrasyonu
* ASP.NET Identity Kullanıcı Yönetimi
