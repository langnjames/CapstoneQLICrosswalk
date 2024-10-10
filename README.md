# VR Rehabilitation Crosswalking Game

A virtual reality application designed to aid patients in relearning or improving their walking abilities through engaging crosswalk simulations. The game integrates with rehabilitation equipment and provides customizable therapy settings for both patients and physical therapists.

## Table of Contents

- [Introduction](#introduction)
- [How to Use](#how-to-use)
  - [Full Project Setup](#full-project-setup)
    - [Prerequisites](#prerequisites)
    - [Installation Steps](#installation-steps)
    - [Running the Application](#running-the-application)
- [Release Notes](#release-notes)
- [Branches Information](#branches-information)

## Introduction

The **VR Rehabilitation Crosswalking Game** is developed to assist patients in rehabilitation by providing a realistic and engaging virtual reality environment. It simulates crosswalk scenarios to help patients regain confidence and improve their walking abilities in real-world situations.

## How to Use

### Full Project Setup

#### Prerequisites

- **VR Headset:** Ensure you have a compatible VR headset (e.g., Oculus Rift, HTC Vive, Meta Quest).
- **Rehabilitation Equipment:** If applicable, set up any required assistance devices.
- **Stable Environment:** Use the VR system in a safe, clutter-free area to prevent accidents.

#### Installation Steps

1. **Set Up VR Hardware:**
   - **Connect the VR Headset:** Follow the manufacturer's instructions to connect your VR headset to your computer.

2. **Download the Application:**
   - **Clone the Repository:**
     ```bash
     git clone https://github.com/yourusername/CapstoneQLICrosswalk.git
     ```
   - **Navigate to the Project Directory:**
     ```bash
     cd CapstoneQLICrosswalk
     ```

3. **Install Dependencies:**
   - **Unity Installation:** Ensure you have Unity installed (version 2022.3.47f1).
   - **Open the Project in Unity:**
     - Launch Unity Hub.
     - Click on **"Add"** and select the cloned project directory.
     - Open the project in Unity Editor.

4. **Build the Application:**
   - **Configure Build Settings:**
     - Go to **File > Build Settings**.
     - Select your target platform (e.g., PC, Oculus).
     - Click **"Build"** and choose the desired output directory.
   - **Run the Application:**
     - Navigate to the build directory and launch the executable file.

#### Running the Application

1. **Launch the Application:**
   - Put on your VR headset and ensure it is properly connected.
   - Start the **VR Rehabilitation Crosswalking Game** application from your computer or VR launcher.


## Release Notes

### **Version 0.1**

**Release Date:** October 12, 2024

**What's Included:**

- **3D Scene Creation:**
  - Developed a basic crosswalk scene with custom-made assets for the crosswalk, sidewalk, street, and characters.
  
- **XR Package Integration:**
  - Added the XR Package into the scene to enable VR functionalities.
  
- **XR Device Simulator:**
  - Incorporated an XR device simulator to test VR scene functionality without needing physical VR hardware.
  
- **Meta Quest Link Functionality:**
  - Ensured the VR device simulator and VR scene operate correctly via Meta Quest Link, allowing seamless testing with Meta Quest devices.

**Known Issues and Limitations:**

- **Gameplay Functionality:**
  - Currently, there is no implementation of cars or interactive gameplay elements. These features are planned for future releases.
  
- **Limited Equipment Integration:**
  - Basic integration with rehabilitation equipment is in place, but full functionality and support for additional devices are pending.

## Branches Information

### **Master Branch**

- **Description:** The main branch containing the stable release of the VR Rehabilitation Crosswalking Game. This branch includes the foundational 3D scenes, asset integration, XR package setup, and initial VR device simulator functionality.
- **Current Status:** Active and up-to-date with the latest features included in Version 0.1.

*Note: At the moment, there are no additional branches such as `development`, `feature-x`, or `experimental`. Future updates may introduce new branches for ongoing development and feature integrations.*

