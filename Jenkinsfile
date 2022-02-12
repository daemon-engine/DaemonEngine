pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                sh 'echo "Building..."'
                sh 'chmod +x Scripts/TestBuild.sh'
                sh 'Scripts/TestBuild.sh'
            }
        }
        stage('Done') {
            steps {
                sh 'echo "Done!"'
            }
        }
    }
}