#!/bin/bash

# Quick script to push BuzzWrite to GitHub
# Repository: https://github.com/kareemmeka/BuzzWrite.git

echo "🚀 Pushing BuzzWrite to GitHub..."
echo ""

# Navigate to project directory
cd "/Users/kareemelsenosy/Documents/CPE - BuzzWrite"

# Initialize git if not already done
if [ ! -d ".git" ]; then
    echo "📦 Initializing Git repository..."
    git init
fi

# Add all files (except those in .gitignore)
echo "➕ Adding files..."
git add .

# Commit
echo "💾 Committing changes..."
git commit -m "Initial commit: BuzzWrite VR handwriting project with all scripts and assets"

# Add remote if not exists
if ! git remote | grep -q "origin"; then
    echo "🔗 Adding remote repository..."
    git remote add origin https://github.com/kareemmeka/BuzzWrite.git
fi

# Rename branch to main if needed
git branch -M main

# Push to GitHub
echo "⬆️  Pushing to GitHub..."
echo "⚠️  You may need to enter your GitHub credentials"
git push -u origin main

echo ""
echo "✅ Done! Check your repository: https://github.com/kareemmeka/BuzzWrite"

