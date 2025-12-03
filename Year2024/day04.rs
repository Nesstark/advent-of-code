use std::fs;

fn main() {
    let input = fs::read_to_string("input/day04input.txt").unwrap();

    part1(&input);
    part2(&input);
}

fn part1(input: &str) {
    let grid: Vec<Vec<char>> = input
        .trim()
        .lines()
        .map(|line| line.chars().collect())
        .collect();
    
    let rows = grid.len();
    let cols = grid[0].len();
    let target = "XMAS";
    
    let directions = [
        (0, 1),
        (1, 0),
        (1, 1),
        (1, -1),
        (0, -1),
        (-1, 0),
        (-1, -1),
        (-1, 1),
    ];
    
    let mut xmas_count = 0;
    
    for row in 0..rows {
        for col in 0..cols {
            for &(dx, dy) in &directions {
                if row as i32 + dx * 3 >= 0 && row as i32 + dx * 3 < rows as i32 &&
                   col as i32 + dy * 3 >= 0 && col as i32 + dy * 3 < cols as i32 {
                    if target.chars().enumerate().all(|(i, c)| {
                        let check_row = (row as i32 + dx * i as i32) as usize;
                        let check_col = (col as i32 + dy * i as i32) as usize;
                        grid[check_row][check_col] == c
                    }) {
                        xmas_count += 1;
                    }
                }
            }
        }
    }
    
    println!("Number of XMAS occurrences: {}", xmas_count);
}

fn part2(input: &str) {
    fn is_mas(
        s: &[Vec<char>],
        (x, y): (isize, isize),
        (dx, dy): (isize, isize),
        (w, h): (isize, isize),
    ) -> bool {
        let valid = |(x, y), (w, h)| 0 <= x && x < w && 0 <= y && y < h;
        let (xn, yn) = (x + dx, y + dy);
        let (xp, yp) = (x - dx, y - dy);

        valid((xn, yn), (w, h))
            && valid((xp, yp), (w, h))
            && s[x as usize][y as usize] == 'A'
            && (s[xp as usize][yp as usize] == 'M' && s[xn as usize][yn as usize] == 'S'
                || s[xp as usize][yp as usize] == 'S' && s[xn as usize][yn as usize] == 'M')
    }

    let (w, h) = (
        input.find("\n").unwrap() as isize,
        input.lines().count() as isize,
    );
    let s = input
        .lines()
        .map(|l| l.chars().collect::<Vec<_>>())
        .collect::<Vec<_>>();

    let mut sum = 0;
    for x in 0..w {
        for y in 0..h {
            let x_mas = is_mas(&s, (x, y), (1, 1), (w, h)) && is_mas(&s, (x, y), (1, -1), (w, h));
            sum += x_mas as u64;
        }
    }
    println!("part 2: {sum}");
}
