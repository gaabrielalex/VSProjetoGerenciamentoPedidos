
delete from item_pedido;

-- Pedido 1
INSERT INTO item_pedido (qtde, vlr_total_item, id_pedido, id_produto) VALUES
(2, 50.00, 1, 5),   -- 2 unidades do produto ID 5, totalizando R$ 50.00
(1, 30.00, 1, 12),  -- 1 unidade do produto ID 12, totalizando R$ 30.00
(2, 5000.00, 1, 6),   -- 2 unidades do produto ID 5, totalizando R$ 50.00
(1, 3000.00, 1, 13);

-- Pedido 2
INSERT INTO item_pedido (qtde, vlr_total_item, id_pedido, id_produto) VALUES
(3, 9200.00, 2, 18),   -- 3 unidades do produto ID 18, totalizando R$ 120.00
(1, 9500.00, 2, 25);    -- 1 unidade do produto ID 25, totalizando R$ 25.00

-- Pedido 3
INSERT INTO item_pedido (qtde, vlr_total_item, id_pedido, id_produto) VALUES
(1, 80.00, 3, 7),    -- 1 unidade do produto ID 7, totalizando R$ 80.00
(2, 45.00, 3, 31);   -- 2 unidades do produto ID 31, totalizando R$ 45.00

-- Pedido 4
INSERT INTO item_pedido (qtde, vlr_total_item, id_pedido, id_produto) VALUES
(4, 150.00, 4, 8),   -- 4 unidades do produto ID 8, totalizando R$ 150.00
(2, 40.00, 4, 20);   -- 2 unidades do produto ID 20, totalizando R$ 40.00

-- Pedido 5
INSERT INTO item_pedido (qtde, vlr_total_item, id_pedido, id_produto) VALUES
(1, 60.00, 5, 15),   -- 1 unidade do produto ID 15, totalizando R$ 60.00
(3, 75.00, 5, 29);   -- 3 unidades do produto ID 29, totalizando R$ 75.00

-- Pedido 6
INSERT INTO item_pedido (qtde, vlr_total_item, id_pedido, id_produto) VALUES
(2, 90.00, 6, 4),    -- 2 unidades do produto ID 4, totalizando R$ 90.00
(1, 20.00, 6, 10);   -- 1 unidade do produto ID 10, totalizando R$ 20.00

-- Pedido 7
INSERT INTO item_pedido (qtde, vlr_total_item, id_pedido, id_produto) VALUES
(3, 100.00, 7, 17),   -- 3 unidades do produto ID 17, totalizando R$ 100.00
(2, 35.00, 7, 22);    -- 2 unidades do produto ID 22, totalizando R$ 35.00

-- Pedido 8
INSERT INTO item_pedido (qtde, vlr_total_item, id_pedido, id_produto) VALUES
(1, 70.00, 8, 2),    -- 1 unidade do produto ID 2, totalizando R$ 70.00
(4, 110.00, 8, 36);  -- 4 unidades do produto ID 36, totalizando R$ 110.00

-- Pedido 9
INSERT INTO item_pedido (qtde, vlr_total_item, id_pedido, id_produto) VALUES
(2, 45.00, 9, 1),    -- 2 unidades do produto ID 1, totalizando R$ 45.00
(3, 80.00, 9, 19);   -- 3 unidades do produto ID 19, totalizando R$ 80.00

-- Pedido 10
INSERT INTO item_pedido (qtde, vlr_total_item, id_pedido, id_produto) VALUES
(1, 25.00, 10, 13),   -- 1 unidade do produto ID 13, totalizando R$ 25.00
(5, 200.00, 10, 30);  -- 5 unidades do produto ID 30, totalizando R$ 200.00

-- Continuar com mais inserções para completar 50 registros...


