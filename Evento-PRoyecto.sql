-- =====================================================
-- CREACIÓN DE ESQUEMAS
-- =====================================================
CREATE SCHEMA auth;
CREATE SCHEMA gestion;
CREATE SCHEMA eventos;
CREATE SCHEMA estadisticas;
CREATE SCHEMA reportes;

-- =====================================================
-- ESQUEMA: auth (Usuarios y Roles)
-- =====================================================
CREATE TABLE auth.usuarios (
    id_usuario SERIAL PRIMARY KEY,
    nombre_completo VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(200) NOT NULL,
    rol VARCHAR(50) NOT NULL, -- Admin, Organizador, Usuario
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- =====================================================
-- ESQUEMA: gestion (Artistas / Grupos)
-- =====================================================
CREATE TABLE gestion.artistas (
    id_artista SERIAL PRIMARY KEY,
    nombre VARCHAR(150) NOT NULL,
    tipo VARCHAR(50),        -- Solista, Grupo, Banda, Ballet, Teatro
    genero VARCHAR(50),      -- Rock, Folklore, Danza, Teatro, etc.
    pais_origen VARCHAR(100),
    descripcion TEXT,
    activo BOOLEAN DEFAULT TRUE
);

-- =====================================================
-- ESQUEMA: eventos (Eventos, Participaciones, Entradas, Ventas, Ganadores)
-- =====================================================
CREATE TABLE eventos.eventos (
    id_evento SERIAL PRIMARY KEY,
    titulo VARCHAR(150) NOT NULL,
    tipo_evento VARCHAR(50), -- Concierto, Feria, Festival, Concurso, Expo
    fecha_inicio TIMESTAMP NOT NULL,
    fecha_fin TIMESTAMP,
    lugar VARCHAR(150),
    descripcion TEXT,
    capacidad INT,
    estado VARCHAR(20) DEFAULT 'PROGRAMADO' -- Programado, En curso, Finalizado, Cancelado
);

CREATE TABLE eventos.participaciones (
    id_participacion SERIAL PRIMARY KEY,
    id_evento INT NOT NULL REFERENCES eventos.eventos(id_evento) ON DELETE CASCADE,
    id_artista INT NOT NULL REFERENCES gestion.artistas(id_artista) ON DELETE CASCADE,
    hora_presentacion TIMESTAMP,
    observaciones TEXT
);

CREATE TABLE eventos.entradas (
    id_entrada SERIAL PRIMARY KEY,
    id_evento INT NOT NULL REFERENCES eventos.eventos(id_evento) ON DELETE CASCADE,
    tipo_entrada VARCHAR(50), -- General, VIP, Estudiante, etc.
    precio DECIMAL(10,2),
    stock INT,
    stock_minimo INT
);

CREATE TABLE eventos.ventas (
    id_venta SERIAL PRIMARY KEY,
    id_evento INT NOT NULL REFERENCES eventos.eventos(id_evento) ON DELETE CASCADE,
    fecha_venta TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    cliente_nombre VARCHAR(100),
    cantidad INT,
    total DECIMAL(10,2)
);

CREATE TABLE eventos.ganadores (
    id_ganador SERIAL PRIMARY KEY,
    id_evento INT NOT NULL REFERENCES eventos.eventos(id_evento) ON DELETE CASCADE,
    id_artista INT NOT NULL REFERENCES gestion.artistas(id_artista) ON DELETE CASCADE,
    puesto VARCHAR(50),   -- 1er lugar, 2do lugar, Mención Especial
    premio VARCHAR(100),
    observaciones TEXT
);

-- =====================================================
-- ESQUEMA: estadisticas (Asistencia y Rendimiento)
-- =====================================================
CREATE TABLE estadisticas.asistencia_evento (
    id_asistencia SERIAL PRIMARY KEY,
    id_evento INT NOT NULL REFERENCES eventos.eventos(id_evento) ON DELETE CASCADE,
    total_asistentes INT,
    porcentaje_ocupacion DECIMAL(5,2)
);

CREATE TABLE estadisticas.rendimiento_artista (
    id_rendimiento SERIAL PRIMARY KEY,
    id_evento INT NOT NULL REFERENCES eventos.eventos(id_evento) ON DELETE CASCADE,
    id_artista INT NOT NULL REFERENCES gestion.artistas(id_artista) ON DELETE CASCADE,
    calificacion DECIMAL(4,2),   -- Evaluación del desempeño
    observaciones TEXT
);

-- =====================================================
-- ESQUEMA: reportes (Historial de reportes)
-- =====================================================
CREATE TABLE reportes.reportes (
    id_reporte SERIAL PRIMARY KEY,
    id_usuario INT NOT NULL REFERENCES auth.usuarios(id_usuario) ON DELETE CASCADE,
    tipo_reporte VARCHAR(50), -- Asistencia, Ventas, Ganadores
    descripcion TEXT,
    fecha_generado TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- =====================================================
-- VISTAS ÚTILES
-- =====================================================
-- Vista de eventos con artistas y participación
CREATE VIEW eventos.v_eventos_participantes AS
SELECT 
    e.id_evento,
    e.titulo,
    e.tipo_evento,
    e.fecha_inicio,
    e.fecha_fin,
    e.lugar,
    a.nombre AS artista,
    a.tipo AS tipo_artista,
    p.hora_presentacion
FROM eventos.eventos e
LEFT JOIN eventos.participaciones p ON e.id_evento = p.id_evento
LEFT JOIN gestion.artistas a ON p.id_artista = a.id_artista;

-- Vista de ventas por evento
CREATE VIEW eventos.v_ventas_evento AS
SELECT 
    ev.id_evento,
    ev.titulo,
    SUM(v.cantidad) AS total_entradas_vendidas,
    SUM(v.total) AS ingresos_totales
FROM eventos.eventos ev
LEFT JOIN eventos.ventas v ON ev.id_evento = v.id_evento
GROUP BY ev.id_evento, ev.titulo;

-- Vista de ganadores por evento
CREATE VIEW eventos.v_ganadores_evento AS
SELECT 
    e.id_evento,
    e.titulo,
    g.puesto,
    a.nombre AS artista,
    g.premio
FROM eventos.eventos e
LEFT JOIN eventos.ganadores g ON e.id_evento = g.id_evento
LEFT JOIN gestion.artistas a ON g.id_artista = a.id_artista;






-- Para eventos
ALTER TABLE eventos.eventos
ALTER COLUMN estado TYPE boolean USING (estado = 'Activo');

-- Para artistas
ALTER TABLE gestion.artistas
ALTER COLUMN activo TYPE boolean USING activo::boolean;
SELECT * FROM gestion.artistas;

