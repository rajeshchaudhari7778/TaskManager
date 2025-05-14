// pipeline {
//     agent any
//     stages {
//         stage('Checkout') {
//             steps {
//                 script {
//                     git branch: 'main', url: 'https://github.com/rajeshchaudhari7778/TaskManager.git'
//                 }
//             }
//         }

//         stage('Restore') {
//             steps {
//                 script {
//                     bat 'dotnet restore'
//                 }
//             }
//         }

//         stage('Build') {
//             steps {
//                 script {
//                     bat 'dotnet clean TaskManager.sln -c Release'
//                     bat 'dotnet build TaskManager.sln -c Release'
//                 }
//             }
//         }

//         stage('Test') {
//             steps {
//                 script {
//                     bat 'dotnet test TaskManager.Tests/TaskManager.Tests.csproj --logger:trx --results-directory TestResults'
//                 }
//             }
//             post {
//                 always {
//                     mstest testResultsFile: 'TestResults/*.trx'
//                 }
//             }
//         }

//         stage('Publish') {
//             steps {
//                 script {
//                     bat 'if exist "%WORKSPACE%\\publish" rd /s /q "%WORKSPACE%\\publish"'
//                     bat 'dotnet publish TaskManager/TaskManager.csproj -c Release -o %WORKSPACE%\\publish --no-restore --no-build /p:CopyOutputSymbolsToPublishDirectory=false'
//                 }
//             }
//         }

//         stage('Deploy') {
//             steps {
//                 script {
//                     bat 'powershell -command "Stop-Website -Name TaskManager"'
//                     bat 'powershell -command "if ((Get-WebsiteState -Name TaskManager).Value -eq \'Started\') { exit 1 }"'
//                     bat 'powershell -command "Restart-WebAppPool -Name TaskManager"'
//                     bat 'powershell -command "Start-Sleep -Seconds 5"'
//                     bat 'if exist "C:\\inetpub\\wwwroot\\TaskManager" rd /s /q "C:\\inetpub\\wwwroot\\TaskManager"'
//                     bat 'mkdir "C:\\inetpub\\wwwroot\\TaskManager"'
//                     bat 'xcopy "%WORKSPACE%\\publish" "C:\\inetpub\\wwwroot\\TaskManager" /E /H /C /I /Y /S /D'
//                     bat 'powershell -command "Start-Website -Name TaskManager"'
//                     bat 'iisreset'
//                 }
//             }
//         }
//     }
// }


pipeline {
    agent any
    parameters {
        booleanParam(name: 'ONLY_DEPLOY', defaultValue: false, description: 'Set to true to skip build stages and deploy to IIS')
    }
    stages {
        stage('Checkout') {
            when {
                expression { !params.ONLY_DEPLOY.toBoolean() }
            }
            steps {
                git branch: 'main', url: 'https://github.com/rajeshchaudhari7778/TaskManager.git'
            }
        }
        stage('Restore') {
            when {
                expression { !params.ONLY_DEPLOY.toBoolean() }
            }
            steps {
                bat 'dotnet restore'
            }
        }
        stage('Build') {
            when {
                expression { !params.ONLY_DEPLOY.toBoolean() }
            }
            steps {
                bat 'dotnet clean TaskManager.sln -c Release'
                bat 'dotnet build TaskManager.sln -c Release'
            }
        }
        stage('Test') {
            when {
                expression { !params.ONLY_DEPLOY.toBoolean() }
            }
            steps {
                bat 'dotnet test TaskManager.Tests/TaskManager.Tests.csproj --logger:trx --results-directory TestResults'
            }
            post {
                always {
                    mstest testResultsFile: 'TestResults/*.trx'
                }
            }
        }
        stage('Publish') {
            when {
                expression { !params.ONLY_DEPLOY.toBoolean() }
            }
            steps {
                bat 'if exist "%WORKSPACE%\\publish" rd /s /q "%WORKSPACE%\\publish"'
                bat 'dotnet publish TaskManager/TaskManager.csproj -c Release -o %WORKSPACE%\\publish --no-restore --no-build /p:CopyOutputSymbolsToPublishDirectory=false'
            }
            post {
                success {
                    writeFile file: 'lastSuccessfulBuild.txt', text: "${env.BUILD_NUMBER}"
                    archiveArtifacts artifacts: 'publish/**,lastSuccessfulBuild.txt', allowEmptyArchive: false
                }
            }
        }
        stage('Deploy') {
            when {
                expression { params.ONLY_DEPLOY.toBoolean() }
            }
            steps {
                script {
                    // Find the last build that has the lastSuccessfulBuild.txt artifact
                    def lastBuildWithArtifacts = null
                    def maxAttempts = 10 // Limit how far back we look
                    def attempt = 0
                    while (lastBuildWithArtifacts == null && attempt < maxAttempts) {
                        try {
                            copyArtifacts projectName: env.JOB_NAME, selector: lastCompleted(offset: attempt), filter: 'lastSuccessfulBuild.txt', target: 'lastBuild'
                            lastBuildWithArtifacts = attempt
                        } catch (Exception e) {
                            attempt++
                            if (attempt == maxAttempts) {
                                error "No build with lastSuccessfulBuild.txt found in the last ${maxAttempts} builds. Cannot proceed with deployment."
                            }
                        }
                    }
                    echo "Found a build with lastSuccessfulBuild.txt at offset ${lastBuildWithArtifacts}"
                    def lastBuildNumber = readFile('lastBuild/lastSuccessfulBuild.txt').trim()
                    echo "Last successful build number: ${lastBuildNumber}"
                    // Copy artifacts from the last successful build using the build number
                    copyArtifacts projectName: env.JOB_NAME, selector: specific(buildNumber: lastBuildNumber), target: 'publish'
                }
                bat 'powershell -command "Stop-Website -Name TaskManager"'
                bat 'powershell -command "if ((Get-WebsiteState -Name TaskManager).Value -eq \'Started\') { exit 1 }"'
                bat 'powershell -command "Restart-WebAppPool -Name TaskManager"'
                bat 'powershell -command "Start-Sleep -Seconds 5"'
                bat 'if exist "C:\\inetpub\\wwwroot\\TaskManager" rd /s /q "C:\\inetpub\\wwwroot\\TaskManager"'
                bat 'mkdir "C:\\inetpub\\wwwroot\\TaskManager"'
                bat 'xcopy "%WORKSPACE%\\publish" "C:\\inetpub\\wwwroot\\TaskManager" /E /H /C /I /Y /S /D'
                bat 'powershell -command "Start-Website -Name TaskManager"'
                bat 'iisreset'
            }
        }
    }
}

//this is running project.

// pipeline {
//     agent any
//     parameters {
//         booleanParam(name: 'DEPLOY', defaultValue: false, description: 'Set to true to deploy the build to IIS')
//     }
//     stages {
//         stage('Checkout') {
//             steps {
//                 git branch: 'main', url: 'https://github.com/rajeshchaudhari7778/TaskManager.git'
//             }
//         }
//         stage('Restore') {
//             steps {
//                 bat 'dotnet restore'
//             }
//         }
//         stage('Build') {
//             steps {
//                 bat 'dotnet clean TaskManager.sln -c Release'
//                 bat 'dotnet build TaskManager.sln -c Release'
//             }
//         }
//         stage('Test') {
//             steps {
//                 bat 'dotnet test TaskManager.Tests/TaskManager.Tests.csproj --logger:trx --results-directory TestResults'
//             }
//             post {
//                 always {
//                     mstest testResultsFile: 'TestResults/*.trx'
//                 }
//             }
//         }
//         stage('Publish') {
//             steps {
//                 bat 'if exist "%WORKSPACE%\\publish" rd /s /q "%WORKSPACE%\\publish"'
//                 bat 'dotnet publish TaskManager/TaskManager.csproj -c Release -o %WORKSPACE%\\publish --no-restore --no-build /p:CopyOutputSymbolsToPublishDirectory=false'
//             }
//             post {
//                 always {
//                     archiveArtifacts artifacts: 'publish/**', allowEmptyArchive: false
//                 }
//             }
//         }
//         stage('Deploy') {
//             when {
//                 expression { params.DEPLOY == true }
//             }
//             steps {
//                 copyArtifacts projectName: env.JOB_NAME, selector: lastSuccessful(), target: 'publish'
//                 bat 'powershell -command "Stop-Website -Name TaskManager"'
//                 bat 'powershell -command "if ((Get-WebsiteState -Name TaskManager).Value -eq \'Started\') { exit 1 }"'
//                 bat 'powershell -command "Restart-WebAppPool -Name TaskManager"'
//                 bat 'powershell -command "Start-Sleep -Seconds 5"'
//                 bat 'if exist "C:\\inetpub\\wwwroot\\TaskManager" rd /s /q "C:\\inetpub\\wwwroot\\TaskManager"'
//                 bat 'mkdir "C:\\inetpub\\wwwroot\\TaskManager"'
//                 bat 'xcopy "%WORKSPACE%\\publish" "C:\\inetpub\\wwwroot\\TaskManager" /E /H /C /I /Y /S /D'
//                 bat 'powershell -command "Start-Website -Name TaskManager"'
//                 bat 'iisreset'
//             }
//         }
//     }
// }
