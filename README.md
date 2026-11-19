# Sistema de Gestión para Vivero

Proyecto integrado de la materia (Univalle): ASP.NET Core MVC (.NET 8) + MySQL/MariaDB (XAMPP) + Entity Framework Core.

| Equipo | Módulos |
|---|---|
| Code³ | Gestión de Personal, Gestión de Plantas |
| FRIENDS | Asignación de Tareas, Paquetes de Decoración |
| Dinamita | Ventas y Pedidos, Gestión de Entregas |
| Mapache | Gestión de Sucursales, Material y Proveedores |

## Inicio rápido
1. Enciende MySQL en XAMPP e importa `database/vivero_db.sql` desde phpMyAdmin (pestaña Importar).
2. Si tu MySQL tiene contraseña, copia `Vivero.Web/appsettings.Local.ejemplo.json` como `appsettings.Local.json` y ponla ahí (con XAMPP no hace falta).
3. Abre `SistemaVivero.sln` en Visual Studio 2022 y presiona F5.
4. Entra con `admin` / `Vivero123*`.

👉 **Antes de programar, lee [GUIA_EQUIPOS.md](GUIA_EQUIPOS.md)**: qué archivo es de quién, ramas de Git y reglas para que el merge no falle.
