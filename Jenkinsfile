pipeline {
    agent any
    stages {
        stage('Compiling') {
            steps {
                sh 'echo "Compiling..."'
                sh 'chmod +x Scripts/Compile.sh'
                sh 'Scripts/Compile.sh'
            }
        }
        stage('Copy files') {
            steps {
                sh 'echo "Copying files..."'
                sh 'chmod +x Scripts/CopyFiles.sh'
                sh 'Scripts/CopyFiles.sh'
            }
        }
        stage('Done') {
            steps {
                sh 'echo "Zipping files..."'
                sh 'chmod +x Scripts/ZipOutput.sh'
                sh 'Scripts/ZipOutput.sh'
                sh 'echo "Done!"'
                archiveArtifacts artifacts: 'DaemonEngine.zip', fingerprint: true
                // archiveArtifacts artifacts: 'Sandbox/bin/Release/net6.0-windows/win10-x64/**', fingerprint: true
            }
        }
    }
}