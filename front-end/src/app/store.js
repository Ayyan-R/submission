import {configureStore} from '@reduxjs/toolkit'
import counterReducer from '../features/counter/counterSlice'
import formReducer from '../features/counter/formSlice'

export default configureStore({
    reducer:{
        counter:counterReducer,
        form:formReducer
    }

})