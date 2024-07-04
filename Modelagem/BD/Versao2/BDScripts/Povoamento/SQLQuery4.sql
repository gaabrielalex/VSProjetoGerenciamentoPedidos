							SELECT p.*, 
							mp.descricao AS descricao_metodo_pagto,
							SUM(ip.vlr_total_item) AS vlr_subtotal
							FROM pedido p, metodo_pagto mp, item_pedido ip
							WHERE p.id_metodo_pagto = mp.id_metodo_pagto
								AND p.id_pedido = ip.id_pedido
								AND p.id_pedido = 10
							GROUP BY p.id_pedido, p.dt_hr_pedido, p.nome_cliente, 
							p.desconto, p.status_pedido, p.observacoes, p.id_metodo_pagto, mp.descricao
							
							insert into item_pedido(id_pedido, id_produto, qtde, vlr_total_item)
							values (10, 4, 4, 115);
							select * from item_pedido where id_pedido = 10

							select * from pedido where id_pedido = 10