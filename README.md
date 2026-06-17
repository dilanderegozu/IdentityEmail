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
### 📸 Yönetim Paneli ve İstatistik Yönetimi

Kullanıcılara sistem aktivitelerini tek bir ekran üzerinden takip edebilme imkânı sunan modern bir yönetim paneli geliştirilmiştir. Dashboard ekranı; kullanıcı istatistikleri, mesaj trafiği, bildirimler ve sistem analizlerini gerçek zamanlı olarak görüntüleyebilecek şekilde tasarlanmıştır.

### Özellikler

* ✅ **Dinamik Dashboard Yapısı:** Kullanıcılara sistem durumunu tek ekranda görüntüleme imkânı sunar.
* ✅ **İstatistik Kartları:** Toplam kullanıcı sayısı, okunmamış mesajlar ve günlük e-posta trafiği gibi veriler anlık olarak gösterilir.
* ✅ **Mesaj Trafik Analizi:** Grafik destekli analiz ekranı ile günlük ve aylık e-posta hareketleri takip edilebilir.
* ✅ **Son Gelen Mesajlar:** Kullanıcının son aldığı mesajlar dashboard üzerinden hızlı şekilde görüntülenebilir.
* ✅ **Akıllı Öneri Sistemi:** Sistem verileri analiz edilerek kullanıcıya özet bilgiler ve öneriler sunulur.
* ✅ **Sistem Duyuruları:** Yönetici tarafından yayınlanan duyurular dashboard üzerinden kullanıcılara iletilir.
* ✅ **Responsive Tasarım:** Tüm dashboard bileşenleri farklı ekran boyutlarına uyumlu şekilde çalışır.

<img src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20023006.png" alt="Dashboard Genel Görünüm" />

<img src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/dashboard" alt="E-Posta Trafik Analizi ve Son Gelen Mesajlar" />

<img src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/dashboard2" alt="Sistem Duyuruları ve Akıllı Öneri Kartları" />

---
## 🔔 Gerçek Zamanlı Bildirim ve Profil Yönetimi

### 📸 Bildirim Merkezi ve Kullanıcı İşlemleri

Kullanıcı deneyimini geliştirmek amacıyla sistem içerisinde dinamik bildirim yönetimi ve hızlı erişim özellikleri geliştirilmiştir. Kullanıcılar gelen mesajlarını anlık olarak takip edebilir, profil işlemlerini tek bir noktadan yönetebilir ve güvenli oturum işlemlerini gerçekleştirebilir.

### Özellikler

* ✅ **Gerçek Zamanlı Bildirim Sistemi:** Gelen kutusundaki son okunmamış mesajlar navbar üzerinden anlık olarak görüntülenir.
* ✅ **Akıllı Bildirim Merkezi:** Kullanıcılara son 3 okunmamış mesajın özeti ve ilgili mesajlara hızlı erişim imkânı sunulur.
* ✅ **Profil Yönetimi:** Kullanıcılar profil bilgilerini görüntüleyebilir ve düzenleme ekranına hızlı şekilde erişebilir.
* ✅ **Hızlı İşlem Menüsü:** Profil, gelen kutusu ve güvenli çıkış (Logout) işlemleri tek bir kullanıcı menüsü altında toplanmıştır.
* ✅ **Dinamik Kullanıcı Bilgileri:** Oturum açan kullanıcının adı, soyadı ve profil fotoğrafı sistem genelinde otomatik olarak görüntülenir.
* ✅ **Kişiselleştirilmiş Arayüz:** Kullanıcıya ait bilgiler tüm sayfalarda dinamik olarak yüklenerek daha kişisel bir deneyim sunulur.
* ✅ **Güvenli Oturum Sonlandırma:** ASP.NET Identity altyapısı kullanılarak güvenli çıkış işlemi gerçekleştirilir.

<img width="392" height="310" alt="Notification Center" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/mesajbox" />

<img width="383" height="448" alt="Profile Menu" src="https://github.com/dilanderegozu/IdentityEmail/blob/master/IdentityEmail/wwwroot/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20014414.png" />


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
