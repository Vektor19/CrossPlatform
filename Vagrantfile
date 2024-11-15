server_ip="192.168.56.11"
Vagrant.configure("2") do |config|

    
    config.vm.define "windows" do |windows|
      windows.vm.box = "gusztavvargadr/windows-10"
      windows.vm.hostname = "windows-vm"
      windows.vm.network :public_network, ip: "192.168.56.10"
      windows.vm.provider "virtualbox" do |vb|
        vb.memory = "4096"
        vb.cpus = 4
      end
      windows.vm.provision "shell", run: "always", inline: <<-SHELL
        # Set execution policy to bypass script restriction
        Set-ExecutionPolicy Bypass -Scope Process -Force

        # Install Chocolatey
        Write-Host "------------------- Installing Chocolatey... -------------------"
        try {
            [System.Net.WebClient]::new().DownloadString('https://chocolatey.org/install.ps1') | Invoke-Expression
        } catch {
            Write-Host "----------------- Failed to install Chocolatey. Exiting... -----------------"
            exit 1
        }

        # Install .NET 8.0 SDK and Runtime
        Write-Host "----------------- Installing .NET 8.0 SDK and Runtime... -----------------"
        choco install dotnet-8.0-sdk -y
        choco install dotnet-8.0-runtime -y
        if (dotnet --version) {
            Write-Host "----------------- .NET Core 8 successfully installed. -----------------"
        } else {
            Write-Host "------------------- Failed to install .NET Core 8. Exiting... ------------------"
            exit 1
        }

        # Verify .NET SDK and Runtime installation
        dotnet --list-sdks
        dotnet --list-runtimes

        # Install .NET Core 3.1 SDK
        Write-Host "------------------- Installing .NET 3.1 SDK -------------------"
        choco install dotnetcore-sdk -y
        if (dotnet --list-sdks | Select-String "3.1") {
            Write-Host "---------------- .NET Core 3.1 SDK successfully installed. ----------------"
        } else {
            Write-Host "---------------- Failed to install .NET Core 3.1 SDK. Exiting... ----------------"
            exit 1
        }

        # Download and extract BaGet
        Write-Host "------------------- Downloading and setting up BaGet... -------------------"
        if (Test-Path "C:/Users/vagrant/baget") {
            Write-Host "---------------------- BaGet is already installed ----------------------"
        } else {
            Invoke-WebRequest -Uri "https://github.com/loic-sharma/BaGet/releases/download/v0.4.0-preview2/BaGet.zip" -OutFile "C:/tmp/BaGet.zip"
            Expand-Archive -Path "C:/tmp/BaGet.zip" -DestinationPath "C:/Users/vagrant/baget" -Force
            Remove-Item "C:/tmp/BaGet.zip"

            if (Test-Path "C:/Users/vagrant/baget") {
                Write-Host "------------------------ BaGet setup complete. ------------------------"
            } else {
                Write-Host "---------------- Failed to set up BaGet. Exiting... ----------------"
                exit 1
            }
        }

        
      
        # Check if BaGet is running
        $response = Invoke-WebRequest -Uri "http://localhost:5000" -UseBasicParsing
        if ($response.StatusCode -eq 200) {
            Write-Host "------------------------- BaGet is already started --------------------------"
        } else {
          
          # Run BaGet
          Write-Host "------------------------- Running BaGet -------------------------"
          cd "C:/Users/vagrant/baget"
          start dotnet BaGet.dll

          try {
            $response2 = Invoke-WebRequest -Uri "http://localhost:5000" -UseBasicParsing
            if ($response2.StatusCode -eq 200) {
                Write-Host "---------------------- BaGet Started successfully ----------------------"
            } else {
                Write-Host "--------------------- Failed to start BaGet. Exiting... ---------------------"
                exit 1
            }
          } catch {
              Write-Host "----------- Failed to connect to BaGet on http://localhost:5000. Exiting... -----------"
              exit 1
          }
        }

        # Add BaGet source and build Lab4
        cd /vagrant/Lab4
        Write-Host "--------------- Configuring BaGet as a NuGet source... ---------------"
        dotnet nuget add source http://localhost:5000/v3/index.json -n BaGet
        if (dotnet nuget list source | Select-String "BaGet") {
            Write-Host "-------------------- BaGet source added successfully --------------------"
        } else {
            Write-Host "---------------- Failed to add BaGet as a NuGet source. Exiting... ----------------"
            exit 1
        }

        Write-Host "--------------------- Building Lab4 project... ---------------------"
        dotnet build
        if (Test-Path "./bin/Debug/VKomissarov.1.0.0.nupkg") {
            Write-Host "--------------------- Lab4 project built successfully ---------------------"
        } else {
            Write-Host "------------------- Failed to build Lab4 project. Exiting... -------------------"
            exit 1
        }

        # Check if package exists in BaGet
        Write-Host "--------------------- Checking if package already exists in BaGet... ---------------------"
        $packageExists = $false
        try {
            $response = Invoke-WebRequest -Uri "http://localhost:5000/v3/registration/vkomissarov/1.0.0.json" -UseBasicParsing
            if ($response.StatusCode -eq 200) {
                $packageExists = $true
                Write-Host "--------------------- Package already exists in BaGet. Skipping push... ---------------------"
            }
        } catch {

        }

        # Push the package to BaGet if it doesn't exist
        if (-not $packageExists) {
                Write-Host "-------------------- Package does not exist. Pushing package to BaGet... --------------------"
            dotnet nuget push -s http://localhost:5000/v3/index.json ./bin/Debug/VKomissarov.1.0.0.nupkg --skip-duplicate
            if ($?) {
                Write-Host "------------------- Package pushed to BaGet successfully -------------------"
            } else {
                Write-Host "------------------- Failed to push package to BaGet. Exiting... -------------------"
                exit 1
            }
        }

        # Install the tool
        Write-Host "---------------- Installing tool VKomissarov globally... ----------------"
        dotnet tool install --global VKomissarov --version 1.0.0
        if (dotnet tool list -g | Select-String "VKomissarov") {
            Write-Host "------------------ Tool VKomissarov installed successfully ------------------"
        } else {
            Write-Host "------------------- Failed to install tool VKomissarov. Exiting... -------------------"
            exit 1
        }

        Write-Host "--------------------- Run 'Lab4' to launch the tool. ---------------------"
      SHELL
    end

    config.vm.define "linux" do |linux|
    linux.vm.box = "hashicorp/bionic64"
    linux.vm.hostname = "linux-vm"
    linux.vm.network "public_network"

    linux.vm.provider "virtualbox" do |vb|
      vb.memory = "4096"
      vb.cpus = 4
    end
    
    linux.vm.provision "shell", run: "always", inline: <<-SHELL
        # Update and install prerequisites
        sudo apt-get update
        sudo apt-get install -y wget apt-transport-https unzip

        # Install .NET SDK 8.0
        wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
        sudo dpkg -i packages-microsoft-prod.deb
        sudo apt-get update
        sudo apt-get install -y dotnet-sdk-8.0

        # Install .NET Core 3.1 SDK
        sudo apt-get install -y dotnet-sdk-3.1

        # Verify .NET SDK and Runtime installations
        dotnet --list-sdks
        dotnet --list-runtimes

        # Download and extract BaGet
        echo "------------------- Setting up BaGet... -------------------"
        if [ -d "/home/vagrant/baget" ]; then
            echo "------------------- BaGet is already installed -------------------"
        else
            wget https://github.com/loic-sharma/BaGet/releases/download/v0.4.0-preview2/BaGet.zip -O BaGet.zip
            unzip BaGet.zip -d /home/vagrant/baget
            rm BaGet.zip
            if [ -d "/home/vagrant/baget" ]; then
                echo "------------------- BaGet setup complete -------------------"
            else
                echo "------------------- Failed to set up BaGet. Exiting... -------------------"
                exit 1
            fi
        fi

        # Check if BaGet is running
        RESPONSE=$(curl -o /dev/null -s -w "%{http_code}" http://localhost:5000)
        if [ "$RESPONSE" -eq 200 ]; then
            echo "------------------- BaGet is already running -------------------"
        else
            # Start BaGet
            echo "------------------- Starting BaGet -------------------"
            cd /home/vagrant/baget
            nohup dotnet BaGet.dll > /dev/null 2>&1 &
            sleep 5
            RESPONSE=$(curl -o /dev/null -s -w "%{http_code}" http://localhost:5000)
            if [ "$RESPONSE" -eq 200 ]; then
                echo "------------------- BaGet started successfully -------------------"
            else
                echo "------------------- Failed to start BaGet. Exiting... -------------------"
                exit 1
            fi
        fi

        # Add BaGet source and build Lab4
        cd /vagrant/Lab4
        echo "--------------- Configuring BaGet as a NuGet source... ---------------"
        dotnet nuget add source http://localhost:5000/v3/index.json -n BaGet
        if dotnet nuget list source | grep -q "BaGet"; then
            echo "------------------- BaGet source added successfully -------------------"
        else
            echo "------------------- Failed to add BaGet as a NuGet source. Exiting... -------------------"
            exit 1
        fi

        echo "------------------- Building Lab4 project... -------------------"
        dotnet build
        if [ -f "./bin/Debug/VKomissarov.1.0.0.nupkg" ]; then
            echo "------------------- Lab4 project built successfully -------------------"
        else
            echo "------------------- Failed to build Lab4 project. Exiting... -------------------"
            exit 1
        fi

        # Check if package exists in BaGet
        echo "------------------- Checking if package exists in BaGet... -------------------"
        PACKAGE_EXISTS=$(curl -s -o /dev/null -w "%{http_code}" http://localhost:5000/v3/registration/vkomissarov/1.0.0.json)
        if [ "$PACKAGE_EXISTS" -eq 200 ]; then
            echo "------------------- Package already exists in BaGet. Skipping push... -------------------"
        else
            echo "------------------- Package does not exist. Pushing package to BaGet... -------------------"
            dotnet nuget push -s http://localhost:5000/v3/index.json ./bin/Debug/VKomissarov.1.0.0.nupkg --skip-duplicate
            if [ $? -eq 0 ]; then
                echo "------------------- Package pushed to BaGet successfully -------------------"
            else
                echo "------------------- Failed to push package to BaGet. Exiting... -------------------"
                exit 1
            fi
        fi

        # Install the tool
        echo "------------------- Installing tool VKomissarov globally... -------------------"
        dotnet tool install VKomissarov --version 1.0.0 --tool-path /bin --add-source http://localhost:5000/v3/index.json
        
        echo 'export PATH=$PATH:/usr/local/go/bin' >> /home/vagrant/.bashrc
        source /home/vagrant/.bashrc
        echo "------------------- Run 'Lab4' to launch the tool. -------------------"
    SHELL
  end

  config.vm.define "lab5" do |linux|
    linux.vm.box = "hashicorp/bionic64"
    linux.vm.hostname = "linux-vm-lab5"
    linux.vm.network "public_network"
    linux.vm.network "forwarded_port", guest: 3000, host: 3000

    linux.vm.provider "virtualbox" do |vb|
      vb.memory = "4096"
      vb.cpus = 4
    end
    
    linux.vm.provision "shell", run: "always", inline: <<-SHELL
        # Update and install prerequisites
        sudo apt-get update
        sudo apt-get install -y wget apt-transport-https unzip

        # Install .NET SDK 8.0
        wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
        sudo dpkg -i packages-microsoft-prod.deb
        sudo apt-get update
        sudo apt-get install -y dotnet-sdk-8.0


        # Verify .NET SDK and Runtime installations
        dotnet --list-sdks
        dotnet --list-runtimes

        
        # Build Lab5
        cd /vagrant/Lab5

        echo "------------------- Building Lab5 project... -------------------"
        dotnet build
        dotnet run
    SHELL
  end

    # config.vm.define "macq" do |mac|
    #   mac.vm.box = "jhcook/macos-sierra"
    #   mac.vm.hostname = "macq-vm"
    #   mac.vm.network "public_network"
    #   mac.vm.provider "virtualbox" do |vb|
    #     vb.memory = "4096"
    #     vb.cpus = 4
    #   end
      
    #   mac.vm.provision "shell", run: "always", privileged: false, inline: <<-SHELL
    #      # Install Homebrew if it's not already installed
    #       #curl -u vagrant:vagrant --anyauth https://developer.apple.com/mac/scripts/downloader.php?path=/ios/iossdk_4.0.2__final/xcode_3.2.3_and_ios_sdk4.0.2.dmg -O -S -v
    #       # sudo xcode-select --install
    #       # touch /tmp/.com.apple.dt.CommandLineTools.installondemand.in-progress
    #       # # sudo softwareupdate -i -a
    #       # sudo softwareupdate --install "Command Line Tools (macOS Sierra version 10.12) for Xcode-9.2"
    #       # # # xcode-select -p

    #       # /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
    #       # brew -v
    #       curl -o dotnet-sdk.pkg "https://download.visualstudio.microsoft.com/download/pr/27a7ece8-f6cd-4cab-89cf-987e85ae6805/2c9ab2cb294143b0533f005640c393da/dotnet-sdk-8.0.100-osx-x64.pkg"
    #       sudo installer -pkg dotnet-sdk.pkg -target /
    #       echo 'export PATH="$PATH:/Users/vagrant/.dotnet"' >> /Users/vagrant/.bash_profile
    #       dotnet --version
    #       # Update Homebrew and install wget and .NET SDK

    #   SHELL
    # end

    
end