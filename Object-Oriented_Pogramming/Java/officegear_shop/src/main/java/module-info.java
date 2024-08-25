module officegear {
    requires javafx.controls;
    requires javafx.fxml;
    requires org.controlsfx.controls;
    requires javafx.graphics;

    opens officegear to javafx.fxml;
    exports officegear;
}
