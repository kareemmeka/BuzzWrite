# BuzzWrite - VR Handwriting Learning App

A VR application for teaching handwriting to children using Meta Quest 2. Users trace letters in 3D space with real-time haptic and visual feedback.

## 🎮 Features

- **26 Uppercase Letters (A-Z)**: Complete alphabet for learning
- **Real-time Haptic Feedback**: Controller vibration when deviating from path
- **Visual Tracing**: Color-coded feedback (blue = on path, red = off path)
- **Drawing System**: Draw trails that follow your hand movement
- **Main Menu**: Simple navigation with instructions
- **Letter Switching**: Easy navigation between letters using controller buttons

## 🛠️ Technologies

- **Unity 2023.2+**
- **Meta XR SDK** (formerly Oculus Integration)
- **Meta Quest 2** (target platform)
- **TextMeshPro** (for 3D letter display)
- **C#** (Unity scripting)

## 📦 Project Structure

```
Assets/
├── Scripts/
│   ├── SimpleHandPointer.cs      # Hand tracking and laser pointer
│   ├── BoundaryDetector.cs       # Path detection and feedback
│   ├── DrawingTrail.cs           # Drawing trail system
│   ├── HapticFeedback.cs         # Controller vibration
│   ├── LetterManager.cs          # Letter generation and display
│   ├── LetterSwitcher.cs        # Letter navigation (A/B buttons)
│   ├── MenuController.cs        # Main menu logic
│   ├── SimpleMenuPointer.cs     # Menu laser pointer
│   ├── SimpleInstructions.cs    # Instructions display
│   └── PersistentOVRManager.cs  # VR manager persistence
├── Scenes/
│   ├── MainMenu                  # Entry scene
│   └── MainScene                 # Gameplay scene
└── BuzzWrite_Assets/
    ├── 3D_Models/                # Letter models and classroom
    ├── Audio/                    # Sound effects
    └── UI_Images/                # UI button images
```

## 🎯 Controls

- **Trigger (Index Finger)** = Draw/Start drawing
- **Grip (Middle Finger)** = Clear all drawings
- **A Button** = Previous letter
- **B Button** = Next letter
- **Point + Trigger** = Interact with menu buttons

## 🚀 Setup Instructions

### Prerequisites
1. Unity 2023.2 or later
2. Meta XR SDK (install via Unity Package Manager)
3. Meta Quest 2 headset
4. Android Build Support (for Unity)

### Installation

1. **Clone this repository:**
   ```bash
   git clone https://github.com/kareemmeka/BuzzWrite.git
   ```

2. **Open in Unity:**
   - Open Unity Hub
   - Click "Add" → Select the cloned project folder
   - Open with Unity 2023.2+

3. **Install Meta XR SDK:**
   - Window → Package Manager
   - Click "+" → "Add package by name"
   - Enter: `com.meta.xr.sdk.all`
   - Click "Add"

4. **Configure for Quest 2:**
   - Edit → Project Settings → XR Plug-in Management
   - Enable "Oculus" under Android
   - Edit → Project Settings → Player → Android
   - Set Minimum API Level to 29
   - Set Target API Level to 33

5. **Build Settings:**
   - File → Build Settings
   - Select Android
   - Add scenes: MainMenu (index 0), MainScene (index 1)
   - Click "Build and Run"

## 📝 How It Works

1. **Hand Tracking**: Uses Meta XR SDK to track right hand position
2. **Letter Display**: TextMeshPro generates 3D letters dynamically
3. **Boundary Detection**: Box collider on letters detects if pen tip is on/off path
4. **Visual Feedback**: Laser pointer changes color (blue/red) based on path adherence
5. **Haptic Feedback**: Controller vibrates when user goes off path
6. **Drawing Trail**: Line renderer creates visual trail when drawing

## 🎓 Usage

1. **Start the app** on Meta Quest 2
2. **Main Menu**: Point laser at "START" button and pull trigger
3. **Positioning**: Stand 2 meters away from the letter
4. **Wait 2 seconds** for haptic test vibration
5. **Draw**: Hold trigger and trace the letter
6. **Feedback**: 
   - Blue = On path (good!)
   - Red = Off path (vibration + red color)
7. **Switch Letters**: Use A/B buttons to navigate
8. **Clear**: Press grip to delete all drawings

## 🔧 Key Scripts

### SimpleHandPointer.cs
Manages the laser pointer that follows your hand. Provides pen tip position for boundary detection.

### BoundaryDetector.cs
Detects if pen tip is within the letter's collider bounds. Triggers visual and haptic feedback.

### DrawingTrail.cs
Creates and manages the drawing trail using LineRenderer. Handles button input for drawing and clearing.

### HapticFeedback.cs
Controls controller vibration intensity and timing.

### LetterManager.cs
Generates 3D letters using TextMeshPro and creates BoxCollider for each letter.

### LetterSwitcher.cs
Manages switching between all 26 letters using A/B controller buttons.

## ⚙️ Configuration

### Adjust Detection Sensitivity
In `BoundaryDetector.cs`:
- `touchDistance`: Distance threshold for on/off path detection (default: 0.03f)

### Adjust Collider Size
In `LetterManager.cs`:
- `colliderWidthMultiplier`: Width of letter collider (default: 0.07f)
- `colliderHeightMultiplier`: Height of letter collider (default: 0.10f)
- `colliderDepth`: Depth of letter collider (default: 0.2f)

### Adjust Laser Appearance
In `SimpleHandPointer.cs`:
- `laserLength`: Length of laser pointer (default: 0.5f)
- `laserWidth`: Width of laser pointer (default: 0.005f)

## 🐛 Troubleshooting

### Scene doesn't load
- Ensure both scenes are in Build Settings
- Check OVRCameraRig is positioned correctly (0,0,0 in MainMenu, 0,0,-2 in MainScene)

### No vibration
- Check HapticFeedback script is attached to a GameObject
- Verify controller is connected
- Wait 3 seconds after scene load (startup delay)

### Letter not detected
- Ensure LetterManager has created the letter GameObject
- Check BoxCollider is attached to letter
- Verify BoundaryDetector has letter reference assigned

### Drawing not working
- Check DrawingTrail script is attached
- Verify trigger button is working (OVRInput)
- Ensure SimpleHandPointer is providing pen tip position

## 👥 Team

- **Kareem Elsenosy** - Developer

## 📄 License

[Specify your license here]

## 🙏 Acknowledgments

- Meta XR SDK team for VR support
- Unity Technologies for the game engine

---

**Built with ❤️ for educational purposes**

