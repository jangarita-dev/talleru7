# Reglas de revisión · pedidos-web

Las usan el subagente `revisor` (en local) y `claude-review.yml` (en cada PR).
Se leen siempre desde `main`: un PR no puede cambiar las reglas que lo revisan.

## Qué es Importante

Marca como **Importante** solo lo que cambiaría lo que el sistema hace o rompería el contrato:

- Lógica incorrecta: un resultado distinto al que pide la spec o el criterio de aceptación.
- Dinero en `double` o `float`. Los montos en pesos (CLP) van siempre en `decimal`.
- Cambios en `legado/` (es de solo lectura) o en `migracion/golden-master.csv`
  sin registrarlos en `docs/migracion/defectos-corregidos.md`.
- Un test que pasaría igual con el código roto (por ejemplo, sin `Assert` o comparando un valor consigo mismo).
- Credenciales, tokens o datos de clientes escritos en el código, en los logs o en los workflows.

Lo demás (nombres, estilo, refactors posibles) es **Detalle** como máximo.

## Tope de detalles

Reporta como máximo **3 Detalles** por revisión. Si hay más, quédate con los 3 más útiles.

## No reportes

- Lo que el CI ya comprueba: que compila y que los tests pasan.
- Formato, espacios y orden de los `using`.
- Archivos de `bin/`, `obj/` y `demos/`.
- Los tests marcados con `Skip` y un motivo («Pendiente U…»): son pendientes registrados a propósito.

## Revisar siempre

1. **Cada criterio de aceptación de la spec tiene su test.** Si el PR implementa una historia
   de `specs/`, cada CA-xx tiene al menos un test que falla si el CA no se cumple, incluidos los límites.
2. **Las entradas inválidas fallan con un mensaje claro.** Un rango invertido, un `null` o una lista vacía
   se manejan como dice la spec (por ejemplo, `ArgumentException` con un mensaje que explique el problema),
   no con un resultado silencioso.
3. **El PR no toca lo que está fuera de alcance.** Ni `legado/`, ni la API, ni `IPedidoRepositorio`,
   ni los datos de ejemplo, salvo que la spec lo pida.

## Cómo reportar

Cada hallazgo lleva `archivo:línea`, qué pasa y en qué caso concreto falla.
Sin evidencia en el código, no lo reportes.
