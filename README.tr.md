<p align="center">
  <img src="docs/images/banner.svg" alt="AFP · Advanced First Person by BerTaDEV" width="100%">
</p>

<p align="center">
  <a href="https://unity.com"><img alt="Unity 2022.3+" src="https://img.shields.io/badge/Unity-2022.3%2B%20%7C%206-000000?logo=unity&logoColor=white"></a>
  <img alt="URP" src="https://img.shields.io/badge/render%20pipeline-URP-2DD4F0">
  <img alt="C#" src="https://img.shields.io/badge/C%23-modular-239120?logo=csharp&logoColor=white">
  <a href="LICENSE"><img alt="MIT License" src="https://img.shields.io/badge/license-MIT-FF8A3D"></a>
  <a href="https://github.com/BerTaDev/Unity-FPS-Controller/releases/latest"><img alt=".unitypackage indir" src="https://img.shields.io/badge/indir-.unitypackage-A78BFA"></a>
</p>

<p align="center">
  <a href="README.md">English</a> · <b>Türkçe</b>
</p>

**AFP (Advanced First Person)**, Unity için modüler bir birinci şahıs kontrolcüsüdür. Tek bir `AdvancedFirstPerson` bileşeni **on bir modülü** çalıştırır: hareket, bakış, zıplama, eğilme, kayma, yana yatma (lean), merdiven, itme, adım sesleri, kamera animasyonu ve hareket efekti. Her modülün kendi `isActive` anahtarı vardır. Kamera; klip tabanlı head bob ve iniş sarsıntısı olan katmanlı bir yapının (rig) üzerinde durur. Paket, hemen oynanabilen bir **URP demo sahnesiyle** gelir.

<p align="center">
  <img src="docs/images/demo.gif" alt="Demo sahnesinde yürüme, koşma, lean ve eğilme" width="100%">
  <br><sub>Demo.unity, Unity 6'da paketin kendi kamera klipleri ve prefab değerleriyle render edildi (betikli oynatım, canlı kayıt değil).</sub>
</p>

---

## Özellikler

<p align="center">
  <img src="docs/images/mechanics.svg" alt="Animasyonlu paneller: koşu ve kamera blend tree, eğilme, kayma, lean, zıplama ve iniş sarsıntısı, merdiven" width="100%">
</p>

| | Modül | Ne yapar | Prefab değerleri |
|---|---|---|---|
| 🏃 | `MovementModule` | Kameraya göre WASD hareketi, Shift ile koşu, eğilirken yarı hız. İsteğe bağlı yumuşatma ve yalnızca ileri koşu | yürüme 3 · koşu 7 m/s |
| 🖱️ | `LookModule` | Fare ile bakış: gövdede yatay, `CameraPivot`'ta dikey dönüş | hassasiyet 2 · sınır ±80° |
| 🦘 | `GravityJumpModule` | Işınla zemin kontrolü, yerçekimi, zıplama (eğilirken daha alçak), iniş sesi ve kamera sarsıntısı | zıplama 1,5 m · yerçekimi −25 |
| 🧎 | `CrouchModule` | Ctrl basılıyken kapsül ve kamera aşağı iner; yukarı atılan ışın tavan altında ayağa kalkmayı engeller | boy 2 → 1 m · kamera 1,8 → 0,7 m |
| 🛝 | `SlidingModule` | Yerde koşarken C: baktığın yöne süreli, eğik kayma | 6 m/s · 0,7 sn |
| ↔️ | `LeaningModule` | Q / E, yapıda aşağıda duran `LeanPivot`'u döndürür; kafa yana savrulur | ±15° · hız 5 |
| 🪜 | `ClimbLadderModule` | İleri ışın tırmanılabilir katmandaki merdiveni bulur; W / S dikey hareket ettirir, yerçekimi kapanır | katman 7 · 1 m · 4 m/s |
| 📦 | `PushModule` | Çarptığın rigidbody'leri iter | itme 3 |
| 👣 | `FootStepSoundsModule` | Yürüme, koşu ve eğilme için adım ritmi; zıplama, iniş ve kayma sesleri | 0,45 / 0,3 / 0,9 sn |
| 🎥 | `CameraAnimationModule` | Kamera Animator'ündeki `moveState` ile Idle, Walk, Run ve CrouchWalk kliplerini harmanlar | geçiş 5 |
| 🌀 | `MovementFxModule` | `MovementFX`'i girdiye ve dikey hıza göre eğer | miktar 2 · yumuşatma 6 |
| 💥 | `Shaker` eklentisi | `ShakePreset` asset'leriyle, giriş/çıkış geçişli konum ve rotasyon sarsıntısı | iniş: 0,2 sn |
| 💬 | `viewPoint` eklentisi | Mesafeye göre beliren, dünyaya sabitli UI ipuçları ("Ladder", "Crouch" …) | 6 m / 3 m |

## Kontroller

<p align="center">
  <img src="docs/images/controls.svg" alt="Kontroller: WASD hareket, fare bakış, Shift koşu, Ctrl eğilme, C kayma, Space zıplama, Q ve E lean" width="100%">
</p>

| Tuş | Eylem |
|---|---|
| `W` `A` `S` `D` | Hareket (3 m/s); merdivende W / S tırmanma |
| Fare | Bakış (dikey ±80°) |
| `Shift` | Koşu (7 m/s) |
| `Ctrl` | Eğilme (basılı tut, 1,5 m/s) |
| `C` | Koşarken kayma (6 m/s, 0,7 sn) |
| `Space` | Zıplama (1,5 m, eğilirken 1 m) |
| `Q` / `E` | Sola / sağa lean (±15°) |

## Demo sahnesi

`Assets/AdvancedFirstPersonSystems/Demo/Scene/Demo.unity`: iki merdiven, eğilme tüneli, zıplama platformları, itilebilir bir top ve her noktada `viewPoint` ipuçları olan bir gray-box parkur.

<table>
  <tr>
    <td width="50%"><img src="docs/images/demo/overview.jpg" alt="Demo seviyesinin genel görünümü"></td>
    <td width="50%"><img src="docs/images/demo/ladder.jpg" alt="Demo sahnesinde merdiven"></td>
  </tr>
  <tr>
    <td width="50%"><img src="docs/images/demo/crouch.jpg" alt="Demo sahnesinde eğilme tüneli"></td>
    <td width="50%"><img src="docs/images/demo/spawn.jpg" alt="Başlangıç noktasından birinci şahıs görünüm ve itilebilir top"></td>
  </tr>
</table>

<sub>Ekran görüntüleri Unity 6000.3'te (URP), Play moduna geçmeden sahneden render edildi.</sub>

## Nasıl çalışır

### Tek bileşen, on bir modül

Her modül; `isActive` bayrağı, `Init(AdvancedFirstPerson)` çağrısı ve bir `Handle…()` metodu olan sade bir `[System.Serializable]` sınıftır. `AdvancedFirstPerson` hepsini `Start`'ta başlatır, `Update`'te sabit sırayla çalıştırır, fare bakışını `LateUpdate`'te yapar ve çarpışmaları itme modülüne verir. Kamera yapısı işi iç içe transform'lara böldüğü için her efekt birbirinden bağımsız kalır.

<p align="center">
  <img src="docs/images/architecture.svg" alt="Çağrı sırasındaki modüller ve sürdükleri prefab transform'ları" width="100%">
</p>

### Hareket durumları

<p align="center">
  <img src="docs/images/states.svg" alt="Durum diyagramı: Idle, Walk, Sprint, Slide, Crouch, Airborne ve Climb ile aralarındaki girdiler" width="100%">
</p>

### Kamera animasyonu

Head bob, `CameraAnimations` üzerindeki dört küçük Animation klibinden gelir ve `moveState` üzerindeki 1D blend tree ile harmanlanır (−1 CrouchWalk, 0 Idle, 1 Walk, 2 Run). `CameraAnimationModule`, `moveState`'i `transitionSpeed` hızında hedefe taşır; böylece yürüme, koşu ve eğilme arasındaki geçişler yumuşak olur.

<p align="center">
  <img src="docs/images/camera-clips.svg" alt="CrouchWalk, Idle, Walk ve Run kamera kliplerinin pitch ve yaw eğrileri" width="100%">
</p>

## Kurulum

### Gereksinimler

| | |
|---|---|
| **Unity** | 2022.3 LTS veya Unity 6 (6000.3'te kontrol edildi) |
| **Render pipeline** | URP. Demo materyalleri URP Lit kullanır; kontrolcü kodu pipeline'dan bağımsızdır |
| **Input** | Eski `Input` sınıfını kullanır. *Project Settings → Player → Active Input Handling* ayarını **Input Manager (Old)** ya da **Both** yapın |
| **TextMesh Pro** | `viewPoint` yazıları TMP kullanır. Unity sorduğunda *TMP Essential Resources*'ı içe aktarın (veya *Window → TextMeshPro*) |

### Yükleme

**Seçenek A — .unitypackage (önerilen)**

1. [Son sürümden](https://github.com/BerTaDev/Unity-FPS-Controller/releases/latest) `AFP-*.unitypackage` dosyasını indirin.
2. Unity'de *Assets → Import Package → Custom Package…* ile hepsini içe aktarın.
3. `Assets/AdvancedFirstPersonSystems/Demo/Scene/Demo.unity` sahnesini açıp **Play**'e basın.

**Seçenek B — kaynağı kopyalayın**

```bash
git clone https://github.com/BerTaDev/Unity-FPS-Controller.git
```

Reponun `Assets/` klasörünü projenize kopyalayın. Prefab ve sahne referanslarının bozulmaması için `.meta` dosyalarını da kopyalayın.

### Kendi sahnenizde kullanım

1. **`Prefabs/ModulerFirstPersonController.prefab`**'ı sahneye sürükleyin ve varsayılan *Main Camera*'yı silin (prefab kendi kamerasını getirir).
2. Yürünebilir zemini **Gravity Jump Module → Ground Layer**'da seçili bir katmana koyun (varsayılan: Default).
3. **Merdiven:** `Prefabs/LadderTrigger.prefab`'ı (veya herhangi bir collider'ı) **7** numaralı katmana koyup merdiven boyunca uzatın. *Tags & Layers*'ta 7. katmana "Ladder" adı vermek isteğe bağlıdır.
4. **İpuçları (isteğe bağlı):** Screen Space bir canvas ekleyin, `ViewPointWorldPrefab` nesneleri yerleştirip yazılarını girin. `Player` etiketli nesneyi ararlar; prefab zaten bu etikete sahip.
5. Modülleri `AdvancedFirstPerson` bileşenindeki `isActive` kutularıyla açıp kapatın.

### Adım sesleri

Ses efektleri bu repoda **yer almıyor**; üçüncü taraf bir paketten geliyorlar ve lisansı yeniden dağıtıma izin vermiyor:

1. Nox_Sound'un ücretsiz **[Footsteps – Essentials](https://assetstore.unity.com/packages/audio/sound-fx/foley/footsteps-essentials-189879)** paketini projenize ekleyin.
2. *Foot Step Sounds Module* üzerinde: `Footsteps_Tile_Walk_01…08` → **Walk Footsteps**, `Footsteps_Tile_Jump_Start_01…03` → **Jump Footsteps**, `Footsteps_Tile_Jump_Land_01…03` → **Land Footsteps**, istediğiniz bir kayma sesi → **Slide Sound Effect**.

Prefab orijinal sesleri GUID ile referans almaya devam ediyor; içe aktardığınız paket aynı GUID'leri koruyorsa yuvalar kendiliğinden dolar. Ses olmadan da her şey çalışır; modül eksik sesleri atlar.

> Demo sahnesindeki *Global Volume*, orijinal pakette bulunmayan bir volume profiline bağlı olduğu için eksik görünür. Herhangi bir profil atayabilir ya da boş bırakabilirsiniz.

## Inspector değerleri ve kendi modülünü yazmak

Tüm alanlar, prefab'daki değerler ve yeni modül yazma örneği için [İngilizce README](README.md#inspector-reference)'ye bakın.

## Lisans

Bu repodaki kod [MIT Lisansı](LICENSE) ile yayınlanmıştır. Üçüncü taraf sesler dahil değildir; bkz. [Adım sesleri](#adım-sesleri).

<p align="center"><sub><b>BerTaDEV</b> tarafından yapıldı · <a href="https://www.bertadev.com">bertadev.com</a></sub></p>
