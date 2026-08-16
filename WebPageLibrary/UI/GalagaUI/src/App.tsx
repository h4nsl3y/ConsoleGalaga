import { useState } from 'react'
import './App.css'
import Area from '@/Component/Area';
import Particles from '@/Component/Particles';
import Navbar from '@/Component/Navbar';

function App() {
  const [showAdmin, setShowAdmin] = useState(false)

  return (
    <>
      <div className='w-full h-screen flex items-center justify-center bg-(--primary-color) relative'>
        <Particles className="absolute"/>
        <div className='h-full w-full absolute'> 
          <Navbar onUserClick={() => setShowAdmin(prev => !prev)}/>
          <Area showAdmin={showAdmin}/>
        </div>
      </div>
    </>
  )
}

export default App
