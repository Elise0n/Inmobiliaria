-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 05-09-2025 a las 02:03:40
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria_db`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `id` int(11) NOT NULL,
  `inmueble_id` int(11) NOT NULL,
  `inquilino_id` int(11) NOT NULL,
  `fecha_inicio` date NOT NULL,
  `fecha_fin` date NOT NULL,
  `monto_mensual` decimal(12,2) NOT NULL,
  `deposito` decimal(12,2) DEFAULT NULL,
  `estado` varchar(20) NOT NULL DEFAULT 'VIGENTE',
  `multa_pct` decimal(5,2) NOT NULL DEFAULT 0.00,
  `observaciones` varchar(500) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1,
  `creado_por` varchar(100) NOT NULL DEFAULT 'sistema',
  `creado_en` datetime NOT NULL DEFAULT current_timestamp(),
  `modificado_por` varchar(100) DEFAULT NULL,
  `modificado_en` datetime DEFAULT NULL,
  `eliminado` tinyint(1) NOT NULL DEFAULT 0,
  `eliminado_por` varchar(100) DEFAULT NULL,
  `eliminado_en` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`id`, `inmueble_id`, `inquilino_id`, `fecha_inicio`, `fecha_fin`, `monto_mensual`, `deposito`, `estado`, `multa_pct`, `observaciones`, `activo`, `creado_por`, `creado_en`, `modificado_por`, `modificado_en`, `eliminado`, `eliminado_por`, `eliminado_en`) VALUES
(1, 3, 1, '2025-01-01', '2026-01-01', 180000.00, 90000.00, 'VIGENTE', 0.00, NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(2, 9, 2, '2024-09-01', '2025-09-01', 190000.00, 95000.00, 'VIGENTE', 0.00, NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(3, 1, 3, '2025-03-01', '2026-03-01', 120000.00, 60000.00, 'VIGENTE', 0.00, NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(4, 4, 4, '2024-11-01', '2025-11-01', 170000.00, 85000.00, 'VIGENTE', 0.00, NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(5, 5, 5, '2023-02-01', '2024-02-01', 300000.00, 150000.00, 'FINALIZADO', 0.00, NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(6, 6, 6, '2025-05-01', '2026-05-01', 130000.00, 65000.00, 'VIGENTE', 0.00, NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(7, 7, 7, '2025-04-01', '2026-04-01', 200000.00, 100000.00, 'VIGENTE', 0.00, NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(8, 2, 8, '2025-06-01', '2026-06-01', 100000.00, 50000.00, 'VIGENTE', 0.00, NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(9, 8, 9, '2024-07-01', '2025-07-01', 150000.00, 75000.00, 'VIGENTE', 0.00, NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(10, 10, 10, '2025-02-01', '2026-02-01', 95000.00, 47500.00, 'VIGENTE', 0.00, NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebles`
--

CREATE TABLE `inmuebles` (
  `id` int(11) NOT NULL,
  `propietario_id` int(11) NOT NULL,
  `direccion` varchar(200) NOT NULL,
  `tipo` varchar(50) NOT NULL,
  `ambientes` int(11) DEFAULT NULL,
  `superficie_m2` decimal(10,2) DEFAULT NULL,
  `precio_base` decimal(12,2) DEFAULT NULL,
  `situacion` varchar(20) NOT NULL DEFAULT 'DISPONIBLE',
  `suspendido_hasta` date DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1,
  `creado_por` varchar(100) NOT NULL DEFAULT 'sistema',
  `creado_en` datetime NOT NULL DEFAULT current_timestamp(),
  `modificado_por` varchar(100) DEFAULT NULL,
  `modificado_en` datetime DEFAULT NULL,
  `eliminado` tinyint(1) NOT NULL DEFAULT 0,
  `eliminado_por` varchar(100) DEFAULT NULL,
  `eliminado_en` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`id`, `propietario_id`, `direccion`, `tipo`, `ambientes`, `superficie_m2`, `precio_base`, `situacion`, `suspendido_hasta`, `activo`, `creado_por`, `creado_en`, `modificado_por`, `modificado_en`, `eliminado`, `eliminado_por`, `eliminado_en`) VALUES
(1, 1, 'San Juan 301', 'Departamento', 2, 45.00, 120000.00, 'DISPONIBLE', NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(2, 2, 'San Juan 302', 'Departamento', 1, 30.00, 100000.00, 'DISPONIBLE', NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(3, 3, 'San Juan 303', 'Casa', 3, 90.00, 200000.00, 'ALQUILADO', NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(4, 4, 'San Juan 304', 'PH', 3, 75.00, 180000.00, 'DISPONIBLE', NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(5, 5, 'San Juan 305', 'Casa', 4, 120.00, 250000.00, 'SUSPENDIDO', NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(6, 6, 'San Juan 306', 'Local', 1, 55.00, 300000.00, 'DISPONIBLE', NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(7, 7, 'San Juan 307', 'Departamento', 2, 50.00, 140000.00, 'DISPONIBLE', NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(8, 8, 'San Juan 308', 'Depósito', 1, 150.00, 500000.00, 'DISPONIBLE', NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(9, 9, 'San Juan 309', 'Casa', 3, 100.00, 230000.00, 'ALQUILADO', NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(10, 10, 'San Juan 310', 'Departamento', 1, 28.00, 95000.00, 'DISPONIBLE', NULL, 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilinos`
--

CREATE TABLE `inquilinos` (
  `id` int(11) NOT NULL,
  `dni` varchar(20) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `telefono` varchar(50) DEFAULT NULL,
  `email` varchar(150) DEFAULT NULL,
  `direccion` varchar(200) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1,
  `creado_por` varchar(100) NOT NULL,
  `creado_en` datetime NOT NULL DEFAULT current_timestamp(),
  `modificado_por` varchar(100) DEFAULT NULL,
  `modificado_en` datetime DEFAULT NULL,
  `eliminado` tinyint(1) NOT NULL DEFAULT 0,
  `eliminado_por` varchar(100) DEFAULT NULL,
  `eliminado_en` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilinos`
--

INSERT INTO `inquilinos` (`id`, `dni`, `nombre`, `apellido`, `telefono`, `email`, `direccion`, `activo`, `creado_por`, `creado_en`, `modificado_por`, `modificado_en`, `eliminado`, `eliminado_por`, `eliminado_en`) VALUES
(1, '30000001', 'Karla', 'Molina', '2644100001', 'karla.molina@mail.com', 'Belgrano 201', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(2, '30000002', 'Luis', 'Nuñez', '2644100002', 'luis.nunez@mail.com', 'Belgrano 202', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(3, '30000003', 'María', 'Acosta', '2644100003', 'maria.acosta@mail.com', 'Belgrano 203', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(4, '30000004', 'Nicolás', 'Bustos', '2644100004', 'nicolas.bustos@mail.com', 'Belgrano 204', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(5, '30000005', 'Olga', 'Domínguez', '2644100005', 'olga.dominguez@mail.com', 'Belgrano 205', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(6, '30000006', 'Pablo', 'Santos', '2644100006', 'pablo.santos@mail.com', 'Belgrano 206', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(7, '30000007', 'Rocío', 'Ríos', '2644100007', 'rocio.rios@mail.com', 'Belgrano 207', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(8, '30000008', 'Sergio', 'Luna', '2644100008', 'sergio.luna@mail.com', 'Belgrano 208', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(9, '30000009', 'Tamara', 'Campos', '2644100009', 'tamara.campos@mail.com', 'Belgrano 209', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(10, '30000010', 'Ulises', 'Peralta', '2644100010', 'ulises.peralta@mail.com', 'Belgrano 210', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `id` int(11) NOT NULL,
  `contrato_id` int(11) NOT NULL,
  `nro_cuota` int(11) NOT NULL,
  `periodo` char(7) NOT NULL,
  `fecha_pago` date DEFAULT NULL,
  `importe` decimal(12,2) NOT NULL,
  `recargo` decimal(12,2) NOT NULL DEFAULT 0.00,
  `estado` varchar(15) NOT NULL DEFAULT 'OK',
  `creado_por` varchar(100) NOT NULL DEFAULT 'sistema',
  `creado_en` datetime NOT NULL DEFAULT current_timestamp(),
  `modificado_por` varchar(100) DEFAULT NULL,
  `modificado_en` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`id`, `contrato_id`, `nro_cuota`, `periodo`, `fecha_pago`, `importe`, `recargo`, `estado`, `creado_por`, `creado_en`, `modificado_por`, `modificado_en`) VALUES
(1, 1, 1, '2025-01', '2025-01-05', 180000.00, 0.00, 'OK', 'seed', '2025-08-31 18:56:21', NULL, NULL),
(2, 1, 2, '2025-02', '2025-02-05', 180000.00, 0.00, 'OK', 'seed', '2025-08-31 18:56:21', NULL, NULL),
(3, 2, 1, '2024-09', '2024-09-10', 190000.00, 0.00, 'OK', 'seed', '2025-08-31 18:56:21', NULL, NULL),
(4, 2, 2, '2024-10', '2024-10-10', 190000.00, 0.00, 'OK', 'seed', '2025-08-31 18:56:21', NULL, NULL),
(5, 3, 1, '2025-03', '2025-03-05', 120000.00, 0.00, 'OK', 'seed', '2025-08-31 18:56:21', NULL, NULL),
(6, 3, 2, '2025-04', '2025-04-05', 120000.00, 0.00, 'OK', 'seed', '2025-08-31 18:56:21', NULL, NULL),
(7, 4, 1, '2024-11', '2024-11-05', 170000.00, 0.00, 'OK', 'seed', '2025-08-31 18:56:21', NULL, NULL),
(8, 5, 12, '2024-01', '2024-01-05', 300000.00, 0.00, 'OK', 'seed', '2025-08-31 18:56:21', NULL, NULL),
(9, 6, 1, '2025-05', '2025-05-05', 130000.00, 0.00, 'OK', 'seed', '2025-08-31 18:56:21', NULL, NULL),
(10, 7, 1, '2025-04', '2025-04-10', 200000.00, 0.00, 'OK', 'seed', '2025-08-31 18:56:21', NULL, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `id` int(11) NOT NULL,
  `dni` varchar(20) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `telefono` varchar(50) DEFAULT NULL,
  `email` varchar(150) DEFAULT NULL,
  `direccion` varchar(200) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1,
  `creado_por` varchar(100) NOT NULL,
  `creado_en` datetime NOT NULL DEFAULT current_timestamp(),
  `modificado_por` varchar(100) DEFAULT NULL,
  `modificado_en` datetime DEFAULT NULL,
  `eliminado` tinyint(1) NOT NULL DEFAULT 0,
  `eliminado_por` varchar(100) DEFAULT NULL,
  `eliminado_en` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`id`, `dni`, `nombre`, `apellido`, `telefono`, `email`, `direccion`, `activo`, `creado_por`, `creado_en`, `modificado_por`, `modificado_en`, `eliminado`, `eliminado_por`, `eliminado_en`) VALUES
(1, '123', 'cosito', 'coso', '2143564u545', 'coso_cosito@mail.com', 'av. siempreviva', 1, 'admin_demo', '2025-08-29 19:58:20', NULL, NULL, 0, NULL, NULL),
(2, '0303456', 'Armando', 'Paredes', '26651234630', 'ArmandoParedes@mail.com', 'Calle de Mentira 458', 1, 'admin_demo', '2025-08-29 20:14:57', NULL, NULL, 0, NULL, NULL),
(3, '40923111', 'Esteban', 'Quito', '2664322222', 'EstebanQuito@mail.com', 'Calle Angosta 5745', 1, 'admin_demo', '2025-08-29 20:15:59', NULL, NULL, 0, NULL, NULL),
(4, '20000001', 'Ana', 'Martínez', '2644000001', 'ana.martinez@mail.com', 'Av. San Martín 101', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(5, '20000002', 'Bruno', 'Gómez', '2644000002', 'bruno.gomez@mail.com', 'Av. San Martín 102', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(6, '20000003', 'Carla', 'López', '2644000003', 'carla.lopez@mail.com', 'Av. San Martín 103', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(7, '20000004', 'Diego', 'Pérez', '2644000004', 'diego.perez@mail.com', 'Av. San Martín 104', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(8, '20000005', 'Eva', 'Suárez', '2644000005', 'eva.suarez@mail.com', 'Av. San Martín 105', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(9, '20000006', 'Fabián', 'Ramírez', '2644000006', 'fabian.ramirez@mail.com', 'Av. San Martín 106', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(10, '20000007', 'Gisela', 'Fernández', '2644000007', 'gisela.fernandez@mail.com', 'Av. San Martín 107', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(11, '20000008', 'Hernán', 'Castro', '2644000008', 'hernan.castro@mail.com', 'Av. San Martín 108', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(12, '20000009', 'Irene', 'Silva', '2644000009', 'irene.silva@mail.com', 'Av. San Martín 109', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL),
(13, '20000010', 'Julián', 'Vega', '2644000010', 'julian.vega@mail.com', 'Av. San Martín 110', 1, 'seed', '2025-08-31 18:56:21', NULL, NULL, 0, NULL, NULL);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_contrato_inmueble` (`inmueble_id`),
  ADD KEY `fk_contrato_inquilino` (`inquilino_id`);

--
-- Indices de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_inm_prop` (`propietario_id`);

--
-- Indices de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_pago_contrato` (`contrato_id`);

--
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD CONSTRAINT `fk_contrato_inmueble` FOREIGN KEY (`inmueble_id`) REFERENCES `inmuebles` (`id`),
  ADD CONSTRAINT `fk_contrato_inquilino` FOREIGN KEY (`inquilino_id`) REFERENCES `inquilinos` (`id`);

--
-- Filtros para la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD CONSTRAINT `fk_inm_prop` FOREIGN KEY (`propietario_id`) REFERENCES `propietarios` (`id`);

--
-- Filtros para la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD CONSTRAINT `fk_pago_contrato` FOREIGN KEY (`contrato_id`) REFERENCES `contratos` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
