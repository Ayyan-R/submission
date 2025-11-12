import React, { useState } from 'react';
import Child from './child';

export default function Parent(){

    const [count, setCount]=useState(0);
    const handleIncrement = ()=>setCount(count+1);
    const handleDecrement = ()=>setCount(count-1);
    return(
        <div style={{textAlign:'center', marginTop:'50px'}}>
            <h1>Parent Component</h1>
                    <Child
                    counter={count}
                    onIncrement={handleIncrement}
                    onDecrement={handleDecrement}
        />
        </div>
    )
}