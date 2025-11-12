import React, { useEffect} from 'react';
import './App.css';

// function App() {
    
// //   const date=new Date();
// //   function formatDate(date){
// //     return Intl.DateTimeFormat(
// //       'en-US',
// //       {weekday:'long'}
// //     ).format(date);
// //     }
//     // const person = {name:"shankara"}
 
// //   const Example03ToDoList=()=>{
// //     return (
// //     <div className="App" style={{backgroundColor:'black', color:'white'}}>
// //         <img src="null" className="App-logo-spin" alt="logo" />
// //         <h1>To Do List {formatDate()} for {person.name}</h1>
// //         <li>
// //              <ul className='todoli'>Physics</ul>
// //              <ul className='todoli'>chemistry</ul>
// //              <ul className='todoli'>Biology</ul>
// //         </li>
// //     </div>)
// //   }

// // const person ={
// //     name:'Gregorio Y, Zara',
// //     imageId:'7vQD0fp',
// //     imageSize:'s',
// //     theme:{
// //         backgroundColor:'black',
// //         color:'pink'
// //     }
// // };
// // const baseUrl  = 'https://i.imgur.com/';
// // function FIX_ERROR_02(){
// //     return (
// //         <div style={person.theme}>
// //             <h1>{person.name}'s Todos</h1>
// //             <img 
// //             className="avatar"
// //             src={`${baseUrl}${person.imageId}${person.imageSize}.jpg`}
// //             alt={person.name}
// //             />
// //             <ul>
// //                 <li>Improve the videophone</li>
// //                 <li>Improve the videophone</li>
// //                 <li>Improve the videophone</li>
// //             </ul>
// //         </div>
// //     )
// // }
// // const {n}=props;

// // return<FIX_ERROR_02/>
// // return <Example03ToDoList/>
// }
// function Welcome({name,age}){
//     console.log(name)
//     console.log(age);
//     return <h2>Hi {name} your age is {age}</h2>
// }

function TimeButton({color}){
    const[time,setTime] = React.useState(new Date().toLocaleTimeString());
    useEffect(()=>{
        const timer = setInterval(()=>{
            setTime(new Date().toLocaleTimeString());
        },1000);
        return ()=>clearInterval(timer);
    },[]);
    return(
        <button
        onClick={()=>setTime(new Date().toLocaleTimeString())}
        style={{color: color, border:"1px solid" + color, padding:"10px 20px", borderRadius:"8px", cursor:"pointer"}}
        >{time}</button>
    )
}

function ColorSelector({selectedColor, onChange}){
    return(
        <select
        value={selectedColor}
        onChange={(e)=>onChange(e.target.value)}
        style={{marginTop:"15px",padding:"5px"}}
        >
            <option value="black">Black</option>
            <option value="red">Red</option>
            <option value="green">Green</option>
            <option value="blue">Blue</option>
            <option value="purple">Purple</option>
        </select>
    )
}

export function TimeColorApp(){
    const[color,setColor]= React.useState("black");
    return(
        <div style={{margin:"10px"}}>
            <TimeButton color={color}/>
            <br/>
            <ColorSelector selectedColor={color} onChange={setColor}/>
        </div>
    )
}

const baseUrl = "https://i.imgur.com"
export function ProfessionList(props){
    const list = props.list;
    const itPeople = list.filter((p)=>p.profession==="IT");
    const nonItPeople = list.filter((p)=>p.profession !=="IT");

    const renderList =(people,title)=>(
        <div style={{marginBottom:"20px"}}>
            <h2>
                {title}({people.length})
            </h2>
                {people.length===0?(
                    <p style={{color:"gray",marginLeft:"10px"}}>No data found</p>
                ):(
                    people.map((person)=>(
                        <div
                        key={person.id}
                        style={{
                            display:"flex",
                            alignItems:"center",
                            marginBottom:"10px",
                        }}
                        >
                        <img
                        src={baseUrl + "/" + person.imageId + person.imageSize + ".jpg"}
                        alt = {person.name}
                        style={{
                            width: "40px",
                            height: "40px",
                            borderRadius: "50%",
                            marginRight: "10px",
                        }}
                        />
                        <p style={{ margin: 0 }}>
                        <strong>{person.name}</strong> — {person.profession} &nbsp;
                        <span style={{ color: "gray" }}>
                        Known for: {person.knownFor}
                        </span>
                        </p>
                    </div>
                    ))
                )}
        </div>
    );
    return (
        <div style={{ fontFamily: "Arial", padding: "20px" }}>
          {renderList(itPeople, "IT Professionals")}
          {renderList(nonItPeople, "Non-IT Professionals")}
        </div>)
}
// export default Welcome;
// export default App;