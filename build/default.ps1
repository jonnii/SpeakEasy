task default -depends Package

task Package -depends Test {

}

task Test -depends Compile, Clean { 
  
}

task Compile -depends Clean { 
  
}

task Clean { 
  
}

