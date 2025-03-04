"use client";

import Image from "next/image";
import { useEffect, useState } from "react";

interface Product {
  id: number;
  name: string;
  price: number;
  stock: number;
  thumbUrl: string;
}

function ProductCard({ id, name, price, stock, thumbUrl }: Product) {
  const [quantity, setQuantity] = useState(1);
  return (
    <div className="border p-4 rounded-lg border-gray-200 hover:border-gray-400 flex  flex-col gap-2">
      <Image
        src={thumbUrl}
        alt={name}
        width={300}
        height={300}
        className="mx-auto object-contain flex-grow"
      />
      <div className="flex flex-col ">
        <div className="font-bold">{name}</div>
        <div className="flex justify-between">
          <div>${price}</div>
          <div className="border-1 p-1 px-2 rounded-md border-orange-400">
            In Stock: {stock}
          </div>
        </div>

        <div>
          Lorem ipsum, dolor sit amet consectetur adipisicing elit. Totam, est.
        </div>
        <form
          method="post"
          onSubmit={(e) => {
            e.preventDefault();
            fetch("http://localhost:8081/orders", {
              method: "POST",
              headers: {
                "Content-Type": "application/json",
              },
              body: JSON.stringify({
                productId: id,
                quantity: quantity,
              }),
            })
              .then((res) => res.json())
              .then((data) => {
                console.log(data);
              });
          }}
          className="flex justify-end gap-3"
        >
          <input type="hidden" name="productId" value={id} />
          <input
            type="number"
            className="border border-gray-200 rounded-md p-1 w-16"
            defaultValue={quantity}
            onChange={(e) => setQuantity(parseInt(e.target.value))}
            name="quantity"
            max={stock}
          />
          <input
            type="submit"
            className="border border-gray-200 rounded-full px-5 p-1 hover:bg-black hover:text-white text-sm"
            value={"Order Now"}
          />
        </form>
      </div>
    </div>
  );
}

export default function Home() {
  const [products, setProducts] = useState<Array<Product>>([]);

  useEffect(() => {
    fetch("http://localhost:8080/products", {
      headers: {
        "Access-Control-Allow-Origin": "*",
      },
    })
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setProducts(data);
      });
  }, []);

  return (
    <div className="p-8">
      <h1 className="text-center text-6xl m-8">Microservices ASP.NET Core</h1>
      <div className="grid lg:grid-cols-4 grid-cols-2 mx-auto w-fit gap-10">
        {products.map((product, key) => (
          <ProductCard
            id={product.id}
            key={key}
            name={product.name}
            price={product.price}
            stock={product.stock}
            thumbUrl={product.thumbUrl}
          />
        ))}
      </div>
    </div>
  );
}
