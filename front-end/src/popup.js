import React from 'react';
import './popup.css';

export default function Popup(props) {
  return (
    <div className="popup-overlay">
      <div className="popup-content">
        <h2>{props.message}</h2>
        <button onClick={props.onClose}>Close</button>
      </div>
    </div>
  );
}
