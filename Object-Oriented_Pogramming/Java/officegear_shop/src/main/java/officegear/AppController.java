package officegear;

import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Cursor;
import javafx.scene.control.*;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.VBox;
import javafx.scene.text.Font;
import javafx.scene.text.Text;
import javafx.scene.text.TextAlignment;
import javafx.scene.text.TextFlow;
import org.controlsfx.control.PopOver;

import java.net.URL;
import java.util.ResourceBundle;

public class AppController implements Initializable {

    double LOGIN_WIDTH = 300;
    double LOGIN_HEIGHT = 420;

    private final UserAuthentication userAuth = new UserAuthentication();

    @FXML
    private Label loginLabel;

    @FXML
    private Label userButton;

    boolean isLoggedIn = false;
    private String loggedInUser = "";

    @FXML
    void loginLabelClicked(MouseEvent event) {
        if (isLoggedIn) {
            loginLabel.setText("Login"); // Wenn wieder ausgeloggt, dann Label zu "Login" ändern
            userButton.setText("User");  // Reset des userButton Textes
            System.out.println("Logout label");
            isLoggedIn = false;
            loggedInUser = "";
        } else {
            showLoginPopOver();
        }
    }

    private PopOver loginPop;
    private PopOver registerPop;

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        // Titel für das Login-Fenster erstellen
        Text title = new Text("Sign In".toUpperCase());
        title.setFont(new Font(25));

        // Benutzername-Label und Eingabefeld erstellen
        Label usernameLabel = new Label("Username:");
        usernameLabel.setFont(new Font(18));
        TextField usernameField = new TextField();
        usernameField.setPrefWidth(LOGIN_WIDTH);
        usernameField.setPrefHeight(55);
        usernameField.setFont(new Font(21));
        usernameField.setStyle("-fx-border-color: grey; -fx-border-width: 2px;");
        usernameField.setPromptText("Enter Username");
        VBox usernameBox = new VBox(5, usernameLabel, usernameField);
        usernameBox.setAlignment(Pos.CENTER_LEFT);

        // Passwort-Label und Eingabefeld erstellen
        Label passwordLabel = new Label("Password:");
        passwordLabel.setFont(new Font(18));
        PasswordField passwordField = new PasswordField();
        passwordField.setPromptText("Enter Password");
        passwordField.setFont(new Font(21));
        passwordField.setStyle("-fx-border-color: grey; -fx-border-width: 2px;");
        passwordField.setPrefWidth(LOGIN_WIDTH);
        passwordField.setPrefHeight(55);
        VBox passwordBox = new VBox(5, passwordLabel, passwordField);
        passwordBox.setAlignment(Pos.CENTER_LEFT);

        // Label für "Passwort vergessen" erstellen und Klick-Ereignis behandeln
        Label forgotPassword = new Label("Forgot password?");
        forgotPassword.setStyle("-fx-underline: true");
        forgotPassword.setFont(new Font(17));
        forgotPassword.setCursor(Cursor.HAND);
        forgotPassword.setOnMouseClicked(event -> {
            String username = usernameField.getText();
            if (!username.isEmpty()) {
                String pass = userAuth.retrievePassword(username);
                if (pass != null) {
                    showAlert(Alert.AlertType.INFORMATION, "Password Recovery", "Your password is " + pass);
                } else {
                    showAlert(Alert.AlertType.ERROR, "Password Recovery", "Username doesn't exist.");
                }
            } else {
                showAlert(Alert.AlertType.ERROR, "Password Recovery", "Please input your username.");
            }
        });

        // Login-Button erstellen und Klick-Ereignis behandeln
        Button signInButton = new Button("Sign In".toUpperCase());
        signInButton.setStyle("-fx-background-color: black; -fx-text-fill: #cfcdcb;");
        signInButton.setCursor(Cursor.HAND);
        signInButton.setFont(new Font(16));
        signInButton.setPrefHeight(68);
        signInButton.setPrefWidth(170);
        signInButton.setOnAction(event -> {
            String username = usernameField.getText();
            String password = passwordField.getText();
            if (userAuth.login(username, password)) {
                showAlert(Alert.AlertType.INFORMATION, "Login Success", "User logged in successfully!");
                loginLabel.setText("Logout");
                userButton.setText(username); // Setze den Text des userButton auf den Benutzernamen
                isLoggedIn = true;
                loggedInUser = username;
                loginPop.hide(); // Schließe das Login-Popover nach erfolgreichem Login
            } else {
                showAlert(Alert.AlertType.ERROR, "Login Failed", "Login failed. Please check your credentials.");
                isLoggedIn = false;
                loggedInUser = "";
            }
        });

        // Registrierungselemente erstellen
        Text newMemberText = new Text("New Member? ");
        Text registerNowText = new Text("Register now");
        registerNowText.setStyle("-fx-underline: true;");
        registerNowText.setCursor(Cursor.HAND);
        TextFlow newMemberFlow = new TextFlow(newMemberText, registerNowText);
        newMemberFlow.setTextAlignment(TextAlignment.CENTER);
        newMemberText.setFont(new Font(16));
        registerNowText.setFont(new Font(16));

        // Layout für das Login-Fenster erstellen
        VBox vbox = new VBox(10);
        vbox.setPadding(new Insets(20));
        vbox.setPrefWidth(LOGIN_WIDTH);
        vbox.setPrefHeight(LOGIN_HEIGHT);
        vbox.setAlignment(Pos.CENTER);
        vbox.getChildren().addAll(
                title,
                usernameBox,
                passwordBox,
                forgotPassword,
                signInButton,
                newMemberFlow
        );
        VBox.setMargin(signInButton, new Insets(10, 0, 20, 0));

        // PopOver für das Login-Fenster erstellen
        loginPop = new PopOver(vbox);
        loginPop.setArrowLocation(PopOver.ArrowLocation.TOP_CENTER);

        // Klick-Ereignis für das Login-Label festlegen
        loginLabel.setOnMouseClicked(this::loginLabelClicked);

        // Titel für das Registrierungsfenster erstellen
        Text registerTitle = new Text("Register".toUpperCase());
        registerTitle.setFont(new Font(25));

        // Benutzername-Label und Eingabefeld für die Registrierung erstellen
        Label registerUsername = new Label("Username:");
        registerUsername.setFont(new Font(18));
        TextField registerUsernameField = new TextField();
        registerUsernameField.setPrefWidth(LOGIN_WIDTH);
        registerUsernameField.setPrefHeight(55);
        registerUsernameField.setFont(new Font(21));
        registerUsernameField.setStyle("-fx-border-color: grey; -fx-border-width: 2px;");
        registerUsernameField.setPromptText("Enter Username");
        VBox registerUsernameBox = new VBox(5, registerUsername, registerUsernameField);
        registerUsernameBox.setAlignment(Pos.CENTER_LEFT);

        // E-Mail-Label und Eingabefeld für die Registrierung erstellen
        Label registerEmailAddress = new Label("E-Mail");
        registerEmailAddress.setFont(new Font(18));
        TextField emailTextField = new TextField();
        emailTextField.setPrefHeight(55);
        emailTextField.setPrefWidth(LOGIN_WIDTH);
        emailTextField.setFont(new Font(21));
        emailTextField.setStyle("-fx-border-color: grey; -fx-border-width: 2px;");
        emailTextField.setPromptText("Enter E-Mail");
        VBox emailBox = new VBox(5, registerEmailAddress, emailTextField);
        emailBox.setAlignment(Pos.CENTER_LEFT);

        // Passwort-Label und Eingabefeld für die Registrierung erstellen
        Label registerPasswordLabel = new Label("Password:");
        registerPasswordLabel.setFont(new Font(18));
        PasswordField registerPasswordField = new PasswordField();
        registerPasswordField.setPromptText("Enter Password");
        registerPasswordField.setFont(new Font(21));
        registerPasswordField.setStyle("-fx-border-color: grey; -fx-border-width: 2px;");
        registerPasswordField.setPrefWidth(LOGIN_WIDTH);
        registerPasswordField.setPrefHeight(55);
        VBox registerPasswordBox = new VBox(5, registerPasswordLabel, registerPasswordField);
        registerPasswordBox.setAlignment(Pos.CENTER_LEFT);

        // Registrierungsbutton erstellen und Klick-Ereignis behandeln
        Button registerButton = new Button("Register".toUpperCase());
        registerButton.setStyle("-fx-background-color: black; -fx-text-fill: #cfcdcb;");
        registerButton.setCursor(Cursor.HAND);
        registerButton.setFont(new Font(16));
        registerButton.setPrefHeight(68);
        registerButton.setPrefWidth(170);
        registerButton.setOnAction(event -> {
            String username = registerUsernameField.getText();
            String email = emailTextField.getText();
            String password = registerPasswordField.getText();

            if (username.isEmpty() || email.isEmpty() || password.isEmpty()) {
                showAlert(Alert.AlertType.ERROR, "Registration Failed", "Please input all fields!");
                return;
            } else if (!PasswordValidator.isValid(password)) {
                showAlert(Alert.AlertType.ERROR, "Registration Failed", "Password must contain at least 8 characters, one uppercase letter, one lowercase letter, and one special character!");
                return;
            }

            if (userAuth.register(username, password, email)) {
                showAlert(Alert.AlertType.INFORMATION, "Registration Success", "User has been registered successfully!");
            } else {
                showAlert(Alert.AlertType.ERROR, "Registration Failed", "Username already exists. Please choose a different username.");
            }
        });

        // Text und Layout für "Bereits ein Konto?"-Nachricht erstellen
        Text alreadyText = new Text("Already have an account? ");
        Text signText = new Text("Sign In");
        signText.setStyle("-fx-underline: true;");
        signText.setCursor(Cursor.HAND);
        TextFlow alreadyFlow = new TextFlow(alreadyText, signText);
        alreadyFlow.setTextAlignment(TextAlignment.CENTER);
        alreadyText.setFont(new Font(16));
        signText.setFont(new Font(16));

        // Layout für das Registrierungsfenster erstellen
        VBox registerVBox = new VBox(10);
        registerVBox.setPadding(new Insets(20));
        registerVBox.setPrefWidth(LOGIN_WIDTH);
        registerVBox.setPrefHeight(LOGIN_HEIGHT + 40);
        registerVBox.setAlignment(Pos.CENTER);
        registerVBox.getChildren().addAll(
                registerTitle,
                registerUsernameBox,
                emailBox,
                registerPasswordBox,
                registerButton,
                alreadyFlow
        );
        registerPop = new PopOver(registerVBox);
        registerPop.setArrowLocation(PopOver.ArrowLocation.TOP_CENTER);

        // Klick-Ereignis für "Jetzt registrieren" festlegen
        registerNowText.setOnMouseClicked(mouseEvent -> {
            loginPop.hide();
            registerPop.show(userButton);
        });

        // Klick-Ereignis für "Sign In" festlegen
        signText.setOnMouseClicked(mouseEvent -> {
            registerPop.hide();
            loginPop.show(userButton);
        });
    }

    // Methode zum Anzeigen des Login-Popovers
    private void showLoginPopOver() {
        loginPop.show(loginLabel);
    }

    // Methode zum Anzeigen von Warnungen
    private void showAlert(Alert.AlertType alertType, String title, String message) {
        Alert alert = new Alert(alertType);
        alert.setTitle(title);
        alert.setHeaderText(null);
        alert.setContentText(message);
        alert.showAndWait();
    }
}
