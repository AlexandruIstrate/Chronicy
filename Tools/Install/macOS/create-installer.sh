#!/bin/sh

VOLUME_NAME="Chronicy Installer"
VOLUME_ICON="assets/volume-icon.png"
BACKGROUND_IMAGE=""
# SOURCE_DIRECTORY="/Users/andu/Library/Developer/Xcode/DerivedData/Chronicy-geosniftoxazmobdkdpfcbepaayo/Build/Products/Debug"
SOURCE_DIRECTORY="bin/"
OUTPUT_FILE_NAME="Chronicy.dmg"

# Delete the old file if it exists
test -f OUTPUT_FILE_NAME && rm OUTPUT_FILE_NAME

# Create the installer
create-dmg \
--volicon $VOLUME_ICON \
--icon-size 128 \
--app-drop-link 100 100 \
$OUTPUT_FILE_NAME \
$SOURCE_DIRECTORY
