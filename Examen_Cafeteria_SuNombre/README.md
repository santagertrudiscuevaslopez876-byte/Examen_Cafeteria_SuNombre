using System;
using System.Collections.Generic;
using System.Text;

namespace Examen_Cafeteria_SuNombre
{
    class README
    {# Cafetería El Buen Sabor - Sistema de Pedidos
# 1. Nombre y matrícula
- Nombre: Samuel Diaz Cuevas
- Matrícula: 2025-3588

# 2. Descripción del programa
Aplicación de escritorio desarrollada en C# Windows Forms (frmPedidos) para registrar productos en un pedido, calcular adicionales por tamaño y porción extra, y obtener el total con descuento, empaque e ITBIS[cite: 1, 10].

# 3. Datos de entrada
- Cliente:** textBox1 [cite: 1, 10]
- Cliente frecuente: checkBox1[cite: 1, 10]
- Fecha:** dateTimePicker1[cite: 1, 10]
- Producto:** textBox2[cite: 1, 10]
- Precio:** textBox3[cite: 1, 10]
- Cantidad:** numericUpDown1[cite: 1, 10]
- Tamaño:** Normal (radioButton1) o Grande +RD$25 (radioButton2)[cite: 1, 10]
- Porción extra:** +RD$30 (checkBox2)[cite: 1, 10]
- Servicio:** Consumir en local (radioButton3) o Para llevar +RD$20 (radioButton4)[cite: 1, 10]

# 4. Proceso
1. Se toma el precio digitado y se suman **RD$25** si se selecciona *Grande* y **RD$30** si se marca *Porción extra*[cite: 1, 10].
2. Se multiplica el precio unitario por la **Cantidad** para obtener el total de cada línea y se acumula el **Subtotal**[cite: 1, 10].
3. Al calcular el total, se aplica un **10% de descuento** sobre el subtotal si el cliente es frecuente[cite: 1, 10].
4. Si el servicio es *Para llevar*, se suman **RD$20** de empaque una sola vez por pedido[cite: 1, 10].
5. Se calcula la base imponible (Subtotal - Descuento + Empaque), el **ITBIS (18%)** sobre esa base y el **Total a pagar** (Base imponible + ITBIS)[cite: 1, 10].

# 5. Salida
- Detalle de los productos agregados en listBox1 con encabezado en label6 [cite: 1, 10].
- Valores formateados en RD$:
  - Subtotal:** label12[cite: 1, 10]
  - Descuento:** label13 [cite: 1, 10]
  - Empaque:** label14 [cite: 1, 10]
  - ITBIS (18%):** label15 [cite: 1, 10]
  - TOTAL A PAGAR:** label16 [cite: 1, 10]

