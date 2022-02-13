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
                sh 'echo "Done!"'
                archiveArtifacts artifacts: 'Sandbox/bin/Release/net6.0/*', fingerprint: true
            }
        }
    }
}