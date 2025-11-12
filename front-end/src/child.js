import React from 'react'
export default function Child(props){
    return(
        <>
        <h2>Counter = {props.counter}</h2>
        <button onClick={props.onIncrement}>Increment</button>
        <button onClick={props.onDecrement}>Decrement</button>
        </>
    )
}