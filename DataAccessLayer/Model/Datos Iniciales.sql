INSERT INTO Usuarios (Email, Pass, Rol, Borrado)
VALUES ('sadmin@trackingfull.com','1','SuperAdministrador', 'False');

INSERT INTO Usuarios (Email, Pass, Rol, Borrado)
VALUES ('admin@trackingfull.com','1','Administrador', 'False');

INSERT INTO Usuarios (Email, Pass, Rol, Borrado)
VALUES ('funcionario@trackingfull.com','1','Funcionario', 'False');

INSERT INTO Usuarios (Email, Pass, Rol, Nombre, Direccion, Telefono, TipoDocumento, NroDocumento, Borrado)
VALUES ('corsaiir@gmail.com','1','Cliente','Mariano Burguete','Calle Falsa 123','092123456','CI','51137298', 'False');

INSERT INTO Usuarios (Email, Pass, Rol, Nombre, Direccion, Telefono, TipoDocumento, NroDocumento, Borrado)
VALUES ('max.la0710@gmail.com','1','Cliente','Maximiliano Langorta','Calle Falsa 321','099876543','CI','87654321', 'False');

INSERT INTO Usuarios (Email, Pass, Rol, Nombre, Direccion, Telefono, TipoDocumento, NroDocumento, Borrado)
VALUES ('leapradoprado1@gmail.com','1','Cliente','Leandro Prado','Calle Falsa 321','099876543','CI','87654321', 'False');

INSERT INTO Usuarios (Email, Pass, Rol, Nombre, Direccion, Telefono, TipoDocumento, NroDocumento, Borrado)
VALUES ('fabionoguera18@gmail.com','1','Cliente','Fabio Noguera','Calle Falsa 321','099876543','CI','87654321', 'False');

INSERT INTO Agencias (Nombre, Direccion, EntregaDomicilio, Borrado)
VALUES ('Agencia CITA San Jose', 'Calle Falsa 123', 'False', 'False');

INSERT INTO Agencias (Nombre, Direccion, EntregaDomicilio, Borrado)
VALUES ('Correo Uruguayo San Jose', 'Calle Falsa 123', 'True', 'False');

INSERT INTO Agencias (Nombre, Direccion, EntregaDomicilio, Borrado)
VALUES ('DAC Montevideo', 'Calle Falsa 123', 'True', 'False');

INSERT INTO Agencias (Nombre, Direccion, EntregaDomicilio, Borrado)
VALUES ('Correo Uruguayo Montevideo', 'Calle Falsa 123', 'True', 'False');

INSERT INTO Agencias (Nombre, Direccion, EntregaDomicilio, Borrado)
VALUES ('TrackingFULL Montevideo', 'Calle Falsa 123', 'False', 'False');

INSERT INTO Trayectos (Nombre, idAgenciaOrigen, idAgenciaDestino, Borrado)
VALUES ('Trayecto 1', 5, 2, 'False');

INSERT INTO PuntosDeControl (Nombre, Posicion, TiempoEstimado, IdTrayecto, Borrado)
VALUES ('Recibido en Origen', 1, 0, 1, 'False')

INSERT INTO PuntosDeControl (Nombre, Posicion, TiempoEstimado, IdTrayecto, Borrado)
VALUES ('Esperando en Origen', 2, 0, 1, 'False')

INSERT INTO PuntosDeControl (Nombre, Posicion, TiempoEstimado, IdTrayecto, Borrado)
VALUES ('En Viaje', 3, 0, 1, 'False')

INSERT INTO PuntosDeControl (Nombre, Posicion, TiempoEstimado, IdTrayecto, Borrado)
VALUES ('Recibido en Destino', 4, 0, 1, 'False')
