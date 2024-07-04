-- Tables
CREATE TABLE metodo_pagto (
    id_metodo_pagto INT NOT NULL IDENTITY(1, 1),
    descricao varchar(50) UNIQUE NOT NULL,
    PRIMARY KEY(id_metodo_pagto)
);

CREATE TABLE produto (
    id_produto INT NOT NULL IDENTITY(1, 1),
    descricao varchar(200) NOT NULL UNIQUE,
    vlr_unitario numeric(8,2) NOT NULL,
    PRIMARY KEY(id_produto)
);

CREATE TABLE pedido (
    id_pedido INT NOT NULL IDENTITY(1, 1),
    nome_cliente varchar(100) NOT NULL,
    desconto numeric(8,2) NULL DEFAULT 0,
    dt_hr_pedido datetime NULL DEFAULT FORMAT(getdate(), 'DD/MM/YYYY'),
    status_pedido char(1) NOT NULL CHECK (status_pedido IN ('A', 'S', 'E', 'C')),
    observacoes varchar(500) NULL default '',
    id_metodo_pagto int NOT NULL,
    PRIMARY KEY(id_pedido),
    FOREIGN KEY(id_metodo_pagto) REFERENCES metodo_pagto (id_metodo_pagto),
);

CREATE TABLE item_pedido (
    id_item_pedido INT IDENTITY(1, 1) NOT NULL,
    qtde int NOT NULL CHECK (qtde > 0),
    vlr_total_item numeric(10,2) NOT NULL CHECK (vlr_total_item > 0),
    id_pedido int NOT NULL,
    id_produto int NOT NULL,
    PRIMARY KEY(id_item_pedido),
    FOREIGN KEY(id_pedido) REFERENCES pedido (id_pedido) ON DELETE CASCADE,
    FOREIGN KEY(id_produto) REFERENCES produto (id_produto)
);


-- Triggers
CREATE or ALTER TRIGGER tr_ItemPedido_InsertOrUpdate
ON item_pedido
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @id_item_pedido INT, @id_pedido INT, @id_produto INT, @qtde INT, @vlr_total_item NUMERIC(10,2);

    -- Loop através das linhas inseridas na tabela inserted
    SELECT @id_pedido = id_pedido,
           @id_produto = id_produto,
           @qtde = qtde,
           @vlr_total_item = vlr_total_item
    FROM inserted;

    -- Verifica se já existe um item com o mesmo id_pedido e id_produto
    IF EXISTS (
        SELECT 1
        FROM item_pedido ip
        WHERE ip.id_pedido = @id_pedido
          AND ip.id_produto = @id_produto
    )
    BEGIN
        -- Se existir, atualiza a quantidade e o valor total
        UPDATE item_pedido
        SET qtde = qtde + @qtde,
            vlr_total_item = vlr_total_item + @vlr_total_item
        WHERE id_pedido = @id_pedido
          AND id_produto = @id_produto;

        -- Retorna o ID do item atualizado
        SELECT id_item_pedido
        FROM item_pedido
        WHERE id_pedido = @id_pedido
          AND id_produto = @id_produto;
    END
    ELSE
    BEGIN
        -- Se não existir, insere um novo item
        INSERT INTO item_pedido (qtde, vlr_total_item, id_pedido, id_produto)
        VALUES (@qtde, @vlr_total_item, @id_pedido, @id_produto);

        -- Retorna o ID do novo item inserido
        SELECT SCOPE_IDENTITY() AS id_item_pedido;
    END
END;
